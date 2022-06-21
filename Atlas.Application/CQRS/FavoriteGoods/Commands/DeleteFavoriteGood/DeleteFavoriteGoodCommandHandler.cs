using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteFavoriteGood
{
    public class DeleteFavoriteGoodCommandHandler : IRequestHandler<DeleteFavoriteGoodCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteFavoriteGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteFavoriteGoodCommand request,
            CancellationToken cancellationToken)
        {
            var favorite = await _dbContext.FavoriteGoods.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (favorite == null || favorite.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(FavoriteGood), request.Id);
            }

            _dbContext.FavoriteGoods.Remove(favorite);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
