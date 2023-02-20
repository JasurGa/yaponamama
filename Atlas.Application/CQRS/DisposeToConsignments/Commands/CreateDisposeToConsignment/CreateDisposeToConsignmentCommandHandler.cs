using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.DisposeToConsignments.Commands.CreateDisposeToConsignment
{
    public class CreateDisposeToConsignmentCommandHandler : IRequestHandler<CreateDisposeToConsignmentCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateDisposeToConsignmentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateDisposeToConsignmentCommand request, CancellationToken cancellationToken)
        {
            var consignment = await _dbContext.Consignments.FirstOrDefaultAsync(x =>
                x.Id == request.ConsignmentId, cancellationToken);

            if (consignment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.ConsignmentId);
            }

            var disposeToGood = new DisposeToConsignment
            {
                Id = Guid.NewGuid(),
                ConsignmentId = request.ConsignmentId,
                Count  = request.Count,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.DisposeToConsignments.AddAsync(disposeToGood, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return disposeToGood.Id;
        }
    }
}
