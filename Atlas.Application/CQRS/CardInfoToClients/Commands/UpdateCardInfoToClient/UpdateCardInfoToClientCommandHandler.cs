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
            var cardInfoToClient = await _dbContext.CardInfoToClients
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cardInfoToClient == null || cardInfoToClient.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(CardInfoToClient), request.Id);
            }    

            cardInfoToClient.CardHolder  = request.CardHolder;
            cardInfoToClient.CardNumber  = request.CardNumber;
            cardInfoToClient.Cvc         = request.Cvc;
            cardInfoToClient.Cvc2        = request.Cvc2;
            cardInfoToClient.DateOfIssue = request.DateOfIssue;
            cardInfoToClient.Name        = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
