using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.UpdateCardInfoToClient
{
    public class UpdateCardInfoToClientCommandHandler : IRequestHandler<UpdateCardInfoToClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateCardInfoToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateCardInfoToClientCommand request, CancellationToken cancellationToken)
        {
            var cardInfoToClient = await _dbContext.CardInfoToClients.FirstOrDefaultAsync(x =>
                x.Number == request.Number && x.ClientId == request.ClientId && 
                x.Id != request.Id, cancellationToken);

            if (cardInfoToClient != null)
            {
                throw new AlreadyExistsException(nameof(CardInfoToClient), request.Number);
            }

            cardInfoToClient = await _dbContext.CardInfoToClients
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cardInfoToClient == null || cardInfoToClient.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(CardInfoToClient), request.Id);
            }    

            cardInfoToClient.Name      = request.Name;
            cardInfoToClient.Number    = request.Number;
            cardInfoToClient.Expire    = request.Expire;
            cardInfoToClient.Token     = request.Token;
            cardInfoToClient.Recurrent = request.Recurrent;
            cardInfoToClient.Verify    = request.Verify;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
