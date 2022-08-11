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
        private readonly IMediator _mediator;

        public UpdateConsignmentCommandHandler(IAtlasDbContext dbContext, IMediator mediator) =>
           (_dbContext, _mediator) = (dbContext, mediator);

        public async Task<Unit> Handle(UpdateConsignmentCommand request,
            CancellationToken cancellationToken)
        {
            var consigment = await _dbContext.Consignments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (consigment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            request.StoreToGood.Id = consigment.StoreToGoodId;
            await _mediator.Send(request.StoreToGood, cancellationToken);

            consigment.ExpirateAt    = request.ExpirateAt;
            consigment.PurchasedAt   = request.PurchasedAt;
            consigment.StoreToGoodId = request.StoreToGood.Id;
            consigment.ShelfLocation = request.ShelfLocation;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
