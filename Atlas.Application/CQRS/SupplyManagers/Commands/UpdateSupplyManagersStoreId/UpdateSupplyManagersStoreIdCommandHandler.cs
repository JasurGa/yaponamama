using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManagersStoreId
{
    public class UpdateSupplyManagersStoreIdCommandHandler :
        IRequestHandler<UpdateSupplyManagersStoreIdCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateSupplyManagersStoreIdCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateSupplyManagersStoreIdCommand request,
            CancellationToken cancellationToken)
        {
            var supplyManagers = await _dbContext.SupplyManagers.Where(x =>
                request.SupplyManagersId.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var supplyManager in supplyManagers)
            {
                supplyManager.StoreId = request.StoreId;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
