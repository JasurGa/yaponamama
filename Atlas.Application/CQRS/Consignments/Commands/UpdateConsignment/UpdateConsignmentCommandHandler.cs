using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment
{
    public class UpdateConsignmentCommandHandler : IRequestHandler<UpdateConsignmentCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateConsignmentCommand request, CancellationToken cancellationToken)
        {
            var consigment = await _dbContext.Consignments
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (consigment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            consigment.ShelfLocation = request.ShelfLocation;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
