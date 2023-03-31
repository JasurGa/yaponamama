using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteAllGoodToCarts
{
    public class DeleteAllGoodToCartsCommandHandler : IRequestHandler<DeleteAllGoodToCartsCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteAllGoodToCartsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteAllGoodToCartsCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var goodToCarts = await _dbContext.GoodToCarts
                .Where(x => x.ClientId == request.ClientId)
                .ToListAsync(cancellationToken);

            _dbContext.GoodToCarts.RemoveRange(goodToCarts);
            return Unit.Value;
        }
    }
}
