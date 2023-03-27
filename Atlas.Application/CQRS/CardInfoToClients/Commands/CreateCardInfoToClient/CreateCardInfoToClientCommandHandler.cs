using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommandHandler
        : IRequestHandler<CreateCardInfoToClientCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateCardInfoToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCardInfoToClientCommand request, CancellationToken cancellationToken)
        {
            var cardInfoToClient = await _dbContext.CardInfoToClients.FirstOrDefaultAsync(x =>
                x.Number == request.Number && x.ClientId == request.ClientId, cancellationToken);

            if (cardInfoToClient != null)
            {
                throw new AlreadyExistsException(nameof(CardInfoToClient), request.Number);
            }

            cardInfoToClient = new CardInfoToClient
            {
                Id          = Guid.NewGuid(),
                ClientId    = request.ClientId,
                Name        = request.Name,
                Number      = request.Number,
                Expire      = request.Expire,
                Token       = request.Token,
                Recurrent   = request.Recurrent,
                Verify      = request.Verify
            };

            await _dbContext.CardInfoToClients.AddAsync(cardInfoToClient,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return cardInfoToClient.Id;
        }
    }
}
