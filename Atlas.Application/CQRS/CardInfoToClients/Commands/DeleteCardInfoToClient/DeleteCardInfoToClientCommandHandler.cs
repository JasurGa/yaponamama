using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.DeleteCardInfoToClient
{
    public class DeleteCardInfoToClientCommandHandler : IRequestHandler<DeleteCardInfoToClientCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteCardInfoToClientCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteCardInfoToClientCommand request, CancellationToken cancellationToken)
        {
            var cardInfoToClient = await _dbContext.CardInfoToClients
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (cardInfoToClient == null || cardInfoToClient.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(CardInfoToClient), request.Id);
            }

            _dbContext.CardInfoToClients.Remove(cardInfoToClient);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
