using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Consignments.Commands.CreateConsignment
{
    public class CreateConsignmentCommandHandler : IRequestHandler<CreateConsignmentCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;
        private readonly IMediator _mediator;

        public CreateConsignmentCommandHandler(IAtlasDbContext dbContext, IMediator mediator) =>
            (_dbContext, _mediator) = (dbContext, mediator);

        public async Task<Guid> Handle(CreateConsignmentCommand request,
            CancellationToken cancellationToken)
        {
            var storeToGoodId = await _mediator.Send(request.StoreToGood, cancellationToken);

            var consignment = new Consignment
            {
                Id              = Guid.NewGuid(),
                ExpirateAt      = request.ExpirateAt,
                PurchasedAt     = request.PurchasedAt,
                ShelfLocation   = request.ShelfLocation,
                StoreToGoodId   = storeToGoodId,
            };

            await _dbContext.Consignments.AddAsync(consignment,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return consignment.Id;
        }
    }
}
