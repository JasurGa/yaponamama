using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.AddressToClients.Commands.UpdateAddressToClient
{
    public class UpdateAddressToClientCommandHandler : IRequestHandler<UpdateAddressToClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateAddressToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateAddressToClientCommand request, CancellationToken cancellationToken)
        {
            var address = await _dbContext.AddressToClients
                .FirstOrDefaultAsync(e => e.Id == request.Id,
                cancellationToken);

            if (address == null || address.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(AddressToClient), request.Id);
            }

            address.Address     = request.Address;
            address.Entrance    = request.Entrance;
            address.Floor       = request.Floor;
            address.Apartment   = request.Apartment;
            address.Latitude    = request.Latitude;
            address.Longitude   = request.Longitude;
            address.AddressType = request.AddressType;
            address.PhoneNumber = request.PhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
