using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.AddressToClients.Commands.DeleteAddressToClient
{
    public class DeleteAddressToClientCommandHandler : IRequestHandler<DeleteAddressToClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteAddressToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteAddressToClientCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.AddressToClients.FirstOrDefaultAsync(
                e => e.Id == request.Id, cancellationToken);

            if (entity == null || entity.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(AddressToClient), request.Id);
            }

            _dbContext.AddressToClients.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
