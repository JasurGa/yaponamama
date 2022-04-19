using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

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
            var cardInfoToClient = new CardInfoToClient
            {
                Id          = Guid.NewGuid(),
                ClientId    = request.ClientId,
                CardHolder  = request.CardHolder,
                CardNumber  = request.CardNumber,
                Cvc         = request.Cvc,
                Cvc2        = request.Cvc2,
                DateOfIssue = request.DateOfIssue,
                Name        = request.Name,
            };

            await _dbContext.CardInfoToClients.AddAsync(cardInfoToClient,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return cardInfoToClient.Id;
        }
    }
}
