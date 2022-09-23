using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

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

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
