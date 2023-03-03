using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var oldAddressToClient = await _dbContext.AddressToClients.FirstOrDefaultAsync(x =>
                x.Address == request.Address &&
                x.ClientId == request.ClientId &&
                x.Entrance == request.Entrance &&
                x.Floor == request.Floor &&
                x.Apartment == request.Apartment);

            if (oldAddressToClient != null)
            {
                return Guid.Empty;
            }

            var addressToClient = new AddressToClient
            {
                Id          = Guid.NewGuid(),
                ClientId    = request.ClientId,
                Address     = request.Address,
                Entrance    = request.Entrance,
                Floor       = request.Floor,
                Apartment   = request.Apartment,
                Latitude    = request.Latitude,
                Longitude   = request.Longitude,
                AddressType = request.AddressType,
                PhoneNumber = request.PhoneNumber,
                CreatedAt   = DateTime.UtcNow,
            };

            await _dbContext.AddressToClients.AddAsync(addressToClient,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return addressToClient.Id;
        }
    }
}
