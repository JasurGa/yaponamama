using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToUser;
using Atlas.Application.CQRS.Notifications.Commands.CreateNotification;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Application.Services;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,
        Guid>
    {
        private readonly static int DELIVERY_PRICE = 10_000;

        private readonly IMapper              _mapper;
        private readonly IMediator            _mediator;
        private readonly IAtlasDbContext      _dbContext;
        private readonly IBotCallbacksService _botCallbacksService;

        public CreateOrderCommandHandler(IMapper mapper, IMediator mediator, IAtlasDbContext dbContext, IBotCallbacksService botCallbacksService) =>
            (_mapper, _mediator, _dbContext, _botCallbacksService) = (mapper, mediator, dbContext, botCallbacksService);

        private async Task<Promo> GetPromoAsync(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Promo == null)
            {
                return null;
            }

            var foundPromo = await _dbContext.Promos.FirstOrDefaultAsync(x =>
                x.Name == request.Promo, cancellationToken);

            if (foundPromo == null || foundPromo.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            if (foundPromo.ClientId != null && foundPromo.ClientId != request.ClientId)
            {
                return null;
            }

            var promoUsage = await _dbContext.PromoUsages.FirstOrDefaultAsync(x =>
                x.PromoId == foundPromo.Id && x.ClientId == request.ClientId);

            if (promoUsage != null)
            {
                return null;
            }

            return foundPromo;
        }

        private async Task<float> GetPurchasePriceAsync(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var calculatedPrice = 0.0f;
            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == createGoodToOrder.GoodId, cancellationToken);

                var priceForGoods = good.PurchasePrice * createGoodToOrder.Count;

                calculatedPrice += priceForGoods;
            }

            return calculatedPrice;
        }

        private async Task<float> GetSellingPriceAsync(CreateOrderCommand request,
            CancellationToken cancellationToken, Promo promo)   
        {
            var calculatedPrice = 0.0f;

            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == createGoodToOrder.GoodId, cancellationToken);

                var priceForGoods = good.SellingPrice * createGoodToOrder.Count *
                    (1 - good.Discount);

                calculatedPrice += priceForGoods;
            }

            if (promo != null)
            {
                calculatedPrice *= (1 - promo.DiscountPercent);
                calculatedPrice -= promo.DiscountPrice;
            }

            return calculatedPrice;
        }

        private async Task<float> GetShippingPriceAsync(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            return request.IsPickup ? 0f : DELIVERY_PRICE;
        }

        private async Task<Store> GetStoreAsync(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var stores = (await _dbContext.Stores
                .ToListAsync(cancellationToken))
                .OrderBy(x => GeoDistance.GetDistance(x.Latitude, x.Longitude,
                    request.ToLatitude, request.ToLongitude));

            Store foundStore = null;
            foreach (var store in stores)
            {
                var found = true;
                foreach (var goodToOrder in request.GoodToOrders)
                {
                    var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                        x.GoodId == goodToOrder.GoodId && x.StoreId == store.Id,
                        cancellationToken);

                    if (storeToGood == null)
                    {
                        found = false;
                        break;
                    }

                    if (storeToGood.Count < goodToOrder.Count)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    foundStore = store;
                    break;
                }
            }

            return foundStore;
        }

        private async Task<Courier> GetCourierAsync(CreateOrderCommand request,
            CancellationToken cancellationToken, Store store)
        {
            var hours = DateTime.UtcNow.Hour;
            if (hours == 0) { hours = 1; }
            var currentRate = (int)Math.Ceiling(hours / 8.0) - 1;

            var minMax = new Dictionary<Courier, int>();

            var couriers = await _dbContext.Couriers.Where(x => x.Vehicle.StoreId == store.Id &&
                x.Rate == currentRate && x.IsDeleted == false)
                .ToListAsync(cancellationToken);

            foreach (var courier in couriers)
            {
                var ordersCount = await _dbContext.Orders.CountAsync(x =>
                    x.Status != (int)OrderStatus.Success &&
                    x.Status != (int)OrderStatus.CanceledByUser &&
                    x.Status != (int)OrderStatus.CanceledByAdmin &&
                    x.CourierId == courier.Id,
                    cancellationToken);

                minMax.Add(courier, ordersCount);
            }

            return minMax.OrderBy(x => x.Value).FirstOrDefault().Key;
        }

        public async Task<Guid> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var foundStore = await GetStoreAsync(request, cancellationToken);
            if (foundStore == null)
            {
                throw new NotFoundException(nameof(Store), request.ClientId.ToString());
            }

            var foundCourier = await GetCourierAsync(request, cancellationToken, foundStore);
            if (foundCourier == null)
            {
                throw new NotFoundException(nameof(Courier), request.ClientId.ToString());
            }

            var foundPromo      = await GetPromoAsync(request, cancellationToken);
            var sellingPrice    = await GetSellingPriceAsync(request, cancellationToken, foundPromo);
            var shippingPrice   = await GetShippingPriceAsync(request, cancellationToken);
            var purchasePrice   = await GetPurchasePriceAsync(request, cancellationToken);

            var order = new Order
            {
                Id                    = Guid.NewGuid(),
                Status                = (int)OrderStatus.Created,
                Comment               = request.Comment,
                DontCallWhenDelivered = request.DontCallWhenDelivered,
                Apartment             = request.Apartment,
                Floor                 = request.Floor,
                Entrance              = request.Entrance,
                Address               = request.Address,
                ToLatitude            = request.ToLatitude,
                ToLongitude           = request.ToLongitude,
                ClientId              = request.ClientId,
                IsPickup              = request.IsPickup,
                PaymentType           = request.PaymentType,
                CreatedAt             = DateTime.UtcNow,
                FinishedAt            = null,
                SellingPrice          = sellingPrice,
                ShippingPrice         = shippingPrice,
                PurchasePrice         = purchasePrice,
                StoreId               = foundStore.Id,
                CourierId             = foundCourier.Id,
                PromoId               = foundPromo != null ? foundPromo.Id : null,
                DeliverAt             = request.DeliverAt,
                IsPrePayed            = false,
                CanRefund             = false,
                IsRefunded            = false,
                TelegramUserId        = request.TelegramUserId,
                IsDevVersionBot       = request.IsDevVersionBot,
                GoodReplacementType   = request.GoodReplacementType,
            };

            await _dbContext.Orders.AddAsync(order,
                cancellationToken);

            if (foundPromo != null)
            {
                await _dbContext.PromoUsages.AddAsync(new PromoUsage
                {
                    Id       = Guid.NewGuid(),
                    ClientId = request.ClientId,
                    PromoId  = foundPromo.Id,
                    UsedAt   = DateTime.UtcNow
                });
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            if (order.TelegramUserId != null)
            {
                await _botCallbacksService.UpdateStatusAsync(order.TelegramUserId.Value, order.IsDevVersionBot,
                    order.Id, order.Status);
            }

            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                createGoodToOrder.OrderId = order.Id;
                createGoodToOrder.StoreId = foundStore.Id;
                await _mediator.Send(createGoodToOrder, cancellationToken);
            }

            var notificationId = await _mediator.Send(new CreateNotificationCommand
            { 
                NotificationTypeId = new Guid("3dc1336a-553b-4869-be74-b771b73e3895"),
                Subject            = "New order",
                Body               = "Hey! There is a new order assigned to you! Please make sure to check the details",
                Priority           = 1
            },
            cancellationToken);

            await _mediator.Send(new AttachNotificationToUserCommand
            {
                UserId         = foundCourier.UserId,
                NotificationId = notificationId
            },
            cancellationToken);

            return order.Id;
        }
    }
}
