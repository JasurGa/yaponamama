using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.CreateFavoriteGood
{
    public class CreateFavoriteGoodCommandHandler : IRequestHandler<CreateFavoriteGoodCommand,
        Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateFavoriteGoodCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateFavoriteGoodCommand request,
            CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.GoodId, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.GoodId);
            }

            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            var entity = new FavoriteGood
            {
                Id        = Guid.NewGuid(),
                GoodId    = request.GoodId,
                ClientId  = request.ClientId,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.FavoriteGoods.AddAsync(entity,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
