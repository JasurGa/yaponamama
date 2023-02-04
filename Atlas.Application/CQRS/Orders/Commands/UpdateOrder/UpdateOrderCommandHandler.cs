using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateOrderCommandHandler(IAtlasDbContext dbContext) =>
            (_dbContext) = (dbContext);

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(x => x.GoodToOrders)
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId,
                cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x => x.Id == request.CourierId,
                cancellationToken);

            if (courier == null)
            {
                throw new NotFoundException(nameof(Courier), request.CourierId);
            }

            var promo = await _dbContext.Promos.FirstOrDefaultAsync(x => x.Id == request.PromoId,
                cancellationToken);

            if (promo == null)
            {
                throw new NotFoundException(nameof(Promo), request.PromoId);
            }

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x => x.Id == request.StoreId,
                cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            if (order.Status == (int)OrderStatus.Success && request.Status != (int)OrderStatus.Success)
            {
                var goodIds = order.GoodToOrders.Select(x => x.GoodId);

                var storeToGoods = await _dbContext.StoreToGoods.Where(x => x.StoreId == order.StoreId &&
                    goodIds.Contains(x.GoodId)).ToListAsync(cancellationToken);

                foreach (var storeToGood in storeToGoods)
                {
                    var goodToOrder = order.GoodToOrders.FirstOrDefault(x => x.GoodId == storeToGood.GoodId);
                    if (goodToOrder != null)
                    {
                        storeToGood.Count += goodToOrder.Count;
                    }
                }
            }
            else if (order.Status != (int)OrderStatus.Success && request.Status == (int)OrderStatus.Success)
            {
                var goodIds = order.GoodToOrders.Select(x => x.GoodId);

                var storeToGoods = await _dbContext.StoreToGoods.Where(x => x.StoreId == order.StoreId &&
                    goodIds.Contains(x.GoodId)).ToListAsync(cancellationToken);

                foreach (var storeToGood in storeToGoods)
                {
                    var goodToOrder = order.GoodToOrders.FirstOrDefault(x => x.GoodId == storeToGood.GoodId);
                    if (goodToOrder != null)
                    {
                        storeToGood.Count -= goodToOrder.Count;
                        if (storeToGood.Count < 0)
                        {
                            storeToGood.Count = 0;
                        }
                    }
                }
            }

            order.CourierId             = request.CourierId;
            order.StoreId               = request.StoreId;
            order.ClientId              = request.ClientId;
            order.Comment               = request.Comment;
            order.DontCallWhenDelivered = request.DontCallWhenDelivered;
            order.Apartment             = request.Apartment;
            order.Floor                 = request.Floor;
            order.Entrance              = request.Entrance;
            order.CreatedAt             = request.CreatedAt;
            order.DeliverAt             = request.DeliverAt;
            order.FinishedAt            = request.FinishedAt;
            order.PurchasePrice         = request.PurchasePrice;
            order.SellingPrice          = request.SellingPrice;
            order.ShippingPrice         = request.ShippingPrice;
            order.Status                = request.Status;
            order.Address               = request.Address;
            order.ToLongitude           = request.ToLongitude;
            order.ToLatitude            = request.ToLatitude;
            order.PaymentType           = request.PaymentType;
            order.IsPickup              = request.IsPickup;
            order.PromoId               = request.PromoId;
            order.CanRefund             = request.CanRefund;
            order.TelegramUserId        = request.TelegramUserId;
            order.IsDevVersionBot       = request.IsDevVersionBot;
            order.GoodReplacementType   = request.GoodReplacementType;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
