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

        public async Task<Unit> Handle(UpdateConsignmentCommand request,
            CancellationToken cancellationToken)
        {
            var consigment = await _dbContext.Consignments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (consigment == null)
            {
                throw new NotFoundException(nameof(Consignment), request.Id);
            }

            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.Id == request.StoreToGoodId, cancellationToken);

            if (storeToGood == null)
            {
                throw new NotFoundException(nameof(StoreToGood), request.StoreToGoodId);
            }

            consigment.ExpirateAt    = request.ExpirateAt;
            consigment.PurchasedAt   = request.PurchasedAt;
            consigment.StoreToGoodId = request.StoreToGoodId;
            consigment.ShelfLocation = request.ShelfLocation;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
