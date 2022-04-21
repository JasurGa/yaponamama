using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {

        private readonly IAtlasDbContext _dbContext;

        public UpdateClientCommandHandler(IAtlasDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if(client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            client.PhoneNumber = request.PhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
