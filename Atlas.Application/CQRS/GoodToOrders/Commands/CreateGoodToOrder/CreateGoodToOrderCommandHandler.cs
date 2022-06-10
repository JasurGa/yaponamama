using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder
{
    public class CreateGoodToOrderCommandHandler : IRequestHandler<CreateGoodToOrderCommand,
        Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateGoodToOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateGoodToOrderCommand request,
            CancellationToken cancellationToken)
        {
            var goodToOrder = new GoodToOrder
            {
                Id      = Guid.NewGuid(),
                GoodId  = request.GoodId,
                OrderId = request.OrderId,
                Count   = request.Count,
            };

            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.StoreId == request.StoreId, cancellationToken);

            storeToGood.Count -= request.Count;

            await _dbContext.GoodToOrders.AddAsync(goodToOrder,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            return goodToOrder.Id;
        }
    }
}
