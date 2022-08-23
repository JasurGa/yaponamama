using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Common.Helpers;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,
        Guid>
    {
        private readonly IMapper         _mapper;
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public CreateOrderCommandHandler(IMapper mapper, IMediator mediator, IAtlasDbContext dbContext) =>
            (_mapper, _mediator, _dbContext) = (mapper, mediator, dbContext);

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
                foundPromo = null;
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
            var couriers = await _dbContext.Couriers.Where(x => x.Vehicle.StoreId == store.Id)
                .ToListAsync(cancellationToken);

            foreach (var courier in couriers)
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                    x.Status == (int)OrderStatus.Created ||
                    x.Status == (int)OrderStatus.Delivering &&
                    x.CourierId == courier.Id,
                    cancellationToken);

                if (order == null)
                {
                    return courier;
                }
            }

            return null;
        }

        private async Task<PaymentType> GetPaymentType(CreateOrderCommand request, CancellationToken cancellation)
        {
            return await _dbContext.PaymentTypes.FirstOrDefaultAsync(x =>
                x.Id == request.PaymentTypeId, cancellation);
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
            var purchasePrice   = await GetPurchasePriceAsync(request, cancellationToken);

            var paymentType     = await GetPaymentType(request, cancellationToken);
            if (paymentType == null)
            {
                throw new NotFoundException(nameof(PaymentType), request.PaymentTypeId);
            }
            
            var order = new Order
            {
                Id                    = Guid.NewGuid(),
                Status                = (int)OrderStatus.Created,
                Comment               = request.Comment,
                DontCallWhenDelivered = request.DontCallWhenDelivered,
                DestinationType       = request.DestinationType,
                Floor                 = request.Floor,
                Entrance              = request.Entrance,
                ToLatitude            = request.ToLatitude,
                ToLongitude           = request.ToLongitude,
                ClientId              = request.ClientId,
                IsPickup              = request.IsPickup,
                PaymentTypeId         = request.PaymentTypeId,
                CreatedAt             = DateTime.UtcNow,
                FinishedAt            = null,
                SellingPrice          = sellingPrice,
                PurchasePrice         = purchasePrice,
                StoreId               = foundStore.Id,
                CourierId             = foundCourier.Id,
                PromoId               = foundPromo != null ? foundPromo.Id : null,
                DeliverAt             = request.DeliverAt
            };

            await _dbContext.Orders.AddAsync(order,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var createGoodToOrder in request.GoodToOrders)
            {
                createGoodToOrder.OrderId = order.Id;
                createGoodToOrder.StoreId = foundStore.Id;
                await _mediator.Send(createGoodToOrder, cancellationToken);
            }

            return order.Id;
        }
    }
}
