using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Commands.DeleteConsignment
{
    public class DeleteConsignmentCommandHandler : IRequestHandler<DeleteConsignmentCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteConsignmentCommand request, CancellationToken cancellationToken)
        {
            var consignment = await _dbContext.Consignments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (consignment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            _dbContext.Consignments.Remove(consignment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
