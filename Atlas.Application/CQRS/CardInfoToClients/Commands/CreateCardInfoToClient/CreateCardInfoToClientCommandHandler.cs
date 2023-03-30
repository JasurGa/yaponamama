using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using Atlas.SubscribeApi.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommandHandler
        : IRequestHandler<CreateCardInfoToClientCommand, Guid>
    {
        private readonly IAtlasDbContext  _dbContext;
        private readonly ISubscribeClient _subscribeClient;

        public CreateCardInfoToClientCommandHandler(IAtlasDbContext dbContext, ISubscribeClient  subscribeClient) =>
            (_dbContext, _subscribeClient) = (dbContext, subscribeClient);

        public async Task<Guid> Handle(CreateCardInfoToClientCommand request, CancellationToken cancellationToken)
        {
            var cardInfo = _subscribeClient.CardsCheck(request.Token);
            if (cardInfo == null)
            {
                throw new NotFoundException(nameof(request.Token), request.Token);
            }

            var cardInfoToClient = await _dbContext.CardInfoToClients.FirstOrDefaultAsync(x =>
                x.Number == cardInfo.card.number && x.ClientId == request.ClientId, cancellationToken);

            if (cardInfoToClient != null)
            {
                throw new AlreadyExistsException(nameof(CardInfoToClient), cardInfo.card.number);
            }

            cardInfoToClient = new CardInfoToClient
            {
                Id          = Guid.NewGuid(),
                ClientId    = request.ClientId,
                Name        = cardInfo.card.number,
                Number      = cardInfo.card.number,
                Expire      = cardInfo.card.expire,
                Token       = cardInfo.card.token,
                Recurrent   = cardInfo.card.recurrent,
                Verify      = cardInfo.card.verify
            };

            await _dbContext.CardInfoToClients.AddAsync(cardInfoToClient,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return cardInfoToClient.Id;
        }
    }
}
