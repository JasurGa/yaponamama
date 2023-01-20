using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGoods
{
    public class DeleteFavoriteGoodsCommandHandler : IRequestHandler<DeleteFavoriteGoodsCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteFavoriteGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteFavoriteGoodsCommand request, CancellationToken cancellationToken)
        {
            var favorites = await _dbContext.FavoriteGoods
                .Where(x => request.FavoriteGoodIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var favorite in favorites)
            {
                if (favorite == null || favorite.ClientId != request.ClientId)
                {
                    throw new NotFoundException(nameof(Client), request.ClientId);
                }
            }

            _dbContext.FavoriteGoods.RemoveRange(favorites);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
