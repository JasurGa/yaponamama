using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient
{
    public class CreateAddressToClientCommandHandler
        : IRequestHandler<CreateAddressToClientCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateAddressToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateAddressToClientCommand request,
            CancellationToken cancellationToken)
        {
            var addressToClient = new AddressToClient
            {
                Id        = Guid.NewGuid(),
                ClientId  = request.ClientId,
                Address   = request.Address,
                Latitude  = request.Latitude,
                Longitude = request.Longitude,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.AddressToClients.AddAsync(addressToClient,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return addressToClient.Id;
        }
    }
}
