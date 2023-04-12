using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.DeleteDisposeToConsignment
{
    public class DeleteDisposeToConsignmentCommandHandler : IRequestHandler<DeleteDisposeToConsignmentCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteDisposeToConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext; 

        public async Task<Unit> Handle(DeleteDisposeToConsignmentCommand request, CancellationToken cancellationToken)
        {
            var disposeToConsignment = await _dbContext.DisposeToConsignments
                .Include(x => x.Consignment.StoreToGood)
                .FirstOrDefaultAsync(x => x.Id == request.Id, 
                    cancellationToken);

            if (disposeToConsignment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            disposeToConsignment.Consignment.StoreToGood.Count += disposeToConsignment.Count;

            _dbContext.DisposeToConsignments.Remove(disposeToConsignment);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
