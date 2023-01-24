using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Commands.RestoreConsignment
{
    public class RestoreConsignmentCommandHandler : IRequestHandler<RestoreConsignmentCommand>
    {
        private readonly IAtlasDbContext _dbContext;
        public RestoreConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreConsignmentCommand request, CancellationToken cancellationToken)
        {
            var consignment = await _dbContext.Consignments
                .Include(x => x.StoreToGood)
                .FirstOrDefaultAsync(x => x.Id == request.Id, 
                    cancellationToken);

            if (consignment == null || !consignment.IsDeleted)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            consignment.IsDeleted = false;

            consignment.StoreToGood.Count += consignment.Count;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
