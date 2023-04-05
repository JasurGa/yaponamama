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

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteAllFavoriteGoods
{
    public class DeleteAllFavoriteGoodsCommandHandler : IRequestHandler<DeleteAllFavoriteGoodsCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteAllFavoriteGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteAllFavoriteGoodsCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var favorites = await _dbContext.FavoriteGoods
                .Where(x => x.ClientId == request.ClientId)
                .ToListAsync(cancellationToken);

            _dbContext.FavoriteGoods.RemoveRange(favorites);
            return Unit.Value;
        }
    }
}
