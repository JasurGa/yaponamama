using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateStoreCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = await _dbContext.Stores.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (store == null)
            {
                throw new NotFoundException(nameof(Store), request.Id);
            }

            store.Name        = request.Name;
            store.NameRu      = request.NameRu;
            store.NameEn      = request.NameEn;
            store.NameUz      = request.NameUz;
            store.Address     = request.Address;
            store.AddressRu   = request.AddressRu;
            store.AddressEn   = request.AddressEn;
            store.AddressUz   = request.AddressUz;
            store.Latitude    = request.Latitude;
            store.Longitude   = request.Longitude;
            store.PhoneNumber = request.PhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
