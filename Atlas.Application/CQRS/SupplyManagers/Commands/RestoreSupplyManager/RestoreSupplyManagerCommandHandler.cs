using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.RestoreSupplyManager
{
    public class RestoreSupplyManagerCommandHandler : IRequestHandler<RestoreSupplyManagerCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public RestoreSupplyManagerCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreSupplyManagerCommand request, CancellationToken cancellationToken)
        {
            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (supplyManager == null || !supplyManager.IsDeleted)
            {
                throw new NotFoundException(nameof(SupplyManager), request.Id);
            }

            supplyManager.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
