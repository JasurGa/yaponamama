using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateManyFavoriteGoods
{
    public class CreateFavoriteGoodsCommandHandler : IRequestHandler<CreateFavoriteGoodsCommand, List<Guid>>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateFavoriteGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        // Just a couple of problems with this solution. I'll work on them later
        // - It doesn't remove any id duplicates.
        // - The NotFoundException doesn't return a list of not found records when thrown. 
        //   I probably gotta extend the exception and CheckIfGoodsExist
        // - There is no check for existing favouriteGoods with the ids provided by request.
        //   (I can't check if FavoriteGood good is similar with one of the ids requested)
        public async Task<List<Guid>> Handle(CreateFavoriteGoodsCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => 
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            if (!CheckIfGoodsExist(request.GoodIds))
            {
                throw new NotFoundException(nameof(Good), request.GoodIds);
            }

            var result = new List<Guid>();
            foreach (var goodId in request.GoodIds)
            {
                var favoriteGood = new FavoriteGood
                {
                    Id          = Guid.NewGuid(),
                    ClientId    = request.ClientId,
                    GoodId      = goodId,
                    CreatedAt   = DateTime.UtcNow,
                };

                await _dbContext.FavoriteGoods.AddAsync(favoriteGood, cancellationToken);

                result.Add(favoriteGood.Id);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }

        private bool CheckIfGoodsExist(IList<Guid> ids)
        {
            var validGoodIds = _dbContext.Goods.Where(x => ids.Contains(x.Id)).Select(x => x.Id);

            var invalidGoodIds = ids.Except(validGoodIds).ToList();

            return invalidGoodIds.Count == 0;
        }
    }
}
