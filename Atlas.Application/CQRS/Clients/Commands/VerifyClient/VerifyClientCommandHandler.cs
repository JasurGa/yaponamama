using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Clients.Commands.VerifyClient
{
    internal class VerifyClientCommandHandler : IRequestHandler<VerifyClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public VerifyClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(VerifyClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            client.IsPassportVerified = true;
            return Unit.Value;
        }
    }
}
