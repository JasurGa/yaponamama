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
        private readonly IAtlasDbContext _dbContext;

        public UpdateSupplyManagerCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateSupplyManagerCommand request, CancellationToken cancellationToken)
        {
            var supplyManager = await _dbContext.SupplyManagers.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (supplyManager == null)
            {
                throw new NotFoundException(nameof(SupplyManager), request.Id);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.StoreId, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.StoreId);
            }

            supplyManager.UserId            = user.Id;
            supplyManager.StoreId           = store.Id;
            supplyManager.PhoneNumber       = request.PhoneNumber;
            supplyManager.PassportPhotoPath = request.PassportPhotoPath;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
