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

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateOrderStatusCommandHandler(IAtlasDbContext dbContext) =>
            (_dbContext) = (dbContext);

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(x => x.GoodToOrders).FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            if (order.Status != (int)OrderStatus.CanceledByAdmin && order.Status != (int)OrderStatus.CanceledByUser)
            {
                if (request.Status == (int)OrderStatus.CanceledByAdmin || request.Status == (int)OrderStatus.CanceledByUser)
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
            }

            order.Status = request.Status;
            if (order.Status == (int)OrderStatus.Success || order.Status == (int)OrderStatus.CanceledByAdmin || order.Status == (int)OrderStatus.CanceledByUser)
            {
                order.FinishedAt = DateTime.UtcNow;
            }
                
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

