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
            var entity = await _dbContext.AddressToClients
                .FirstOrDefaultAsync(e => e.Id == request.Id,
                cancellationToken);

            if (entity == null || entity.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(AddressToClient), request.Id);
            }

            entity.Address   = request.Address;
            entity.Latitude  = request.Latitude;
            entity.Longitude = request.Longitude;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
