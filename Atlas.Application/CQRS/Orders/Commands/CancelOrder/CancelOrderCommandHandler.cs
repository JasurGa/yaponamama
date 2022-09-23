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

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public CancelOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CancelOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.Include(x => x.GoodToOrders)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null || order.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            order.Status     = (int)OrderStatus.CanceledByAdmin;
            order.FinishedAt = DateTime.UtcNow;

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
                
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
