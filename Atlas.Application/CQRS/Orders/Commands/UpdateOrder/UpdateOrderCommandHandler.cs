using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
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
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), order.Id);
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId,
                cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), client.Id);
            }

            var courier = await _dbContext.Couriers.FirstOrDefaultAsync(x => x.Id == request.CourierId,
                cancellationToken);

            if (courier == null)
            {
                throw new NotFoundException(nameof(Courier), courier.Id);
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
            order.Status                = request.Status;
            order.ToLongitude           = request.ToLongitude;
            order.ToLatitude            = request.ToLatitude;
            order.PaymentType           = request.PaymentType;
            order.IsPickup              = request.IsPickup;
            order.PromoId               = request.PromoId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
