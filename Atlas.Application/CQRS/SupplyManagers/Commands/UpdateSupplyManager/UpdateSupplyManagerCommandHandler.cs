using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager
{
    public class UpdateSupplyManagerCommandHandler : IRequestHandler<UpdateSupplyManagerCommand>
    {
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public UpdateSupplyManagerCommandHandler(IMediator mediator, IAtlasDbContext dbContext) =>
            (_mediator, _dbContext) = (mediator, dbContext);

        public async Task<Unit> Handle(UpdateSupplyManagerCommand request, CancellationToken cancellationToken)
        {
            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (supplyManager == null)
            {
                throw new NotFoundException(nameof(SupplyManager), request.Id);
            }

            request.User.Id = supplyManager.UserId;
            await _mediator.Send(request.User);

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            supplyManager.StoreId           = store.Id;
            supplyManager.PhoneNumber       = request.PhoneNumber;
            supplyManager.PassportPhotoPath = request.PassportPhotoPath;
            supplyManager.Salary            = request.Salary;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
