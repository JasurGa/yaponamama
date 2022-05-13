using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood
{
    public class CreateStoreToGoodCommandHandler : IRequestHandler<CreateStoreToGoodCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateStoreToGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateStoreToGoodCommand request, CancellationToken cancellationToken)
        {
            var storeToGood = new StoreToGood
            {
                Id      = Guid.NewGuid(),
                StoreId = request.StoreId,
                GoodId  = request.GoodId,
                Count   = request.Count,
            };

            await _dbContext.StoreToGoods.AddAsync(storeToGood, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return storeToGood.Id;
        }
    }
}
