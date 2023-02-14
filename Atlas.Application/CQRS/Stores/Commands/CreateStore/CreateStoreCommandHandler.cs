using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.Stores.Commands.CreateStore
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateStoreCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var store = new Store
            {
                Id             = Guid.NewGuid(),
                Name           = request.Name,
                NameRu         = request.NameRu,
                NameEn         = request.NameEn,
                NameUz         = request.NameUz,
                Address        = request.Address,
                AddressRu      = request.AddressRu,
                AddressEn      = request.AddressEn,
                AddressUz      = request.AddressUz,
                PhoneNumber    = request.PhoneNumber,
                Latitude       = request.Latitude,
                Longitude      = request.Longitude,
                WorkStartsAt   = request.WorkStartsAt,
                WorkFinishesAt = request.WorkFinishesAt,
                IsDeleted      = false,
            };

            await _dbContext.Stores.AddAsync(store,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return store.Id;
        }
    }
}
