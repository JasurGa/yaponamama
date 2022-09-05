using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Clients.Commands.RestoreClient
{
    public class RestoreClientCommandHandler : IRequestHandler<RestoreClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;
        public RestoreClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RestoreClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (client == null || !client.IsDeleted)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            client.IsDeleted = false;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
