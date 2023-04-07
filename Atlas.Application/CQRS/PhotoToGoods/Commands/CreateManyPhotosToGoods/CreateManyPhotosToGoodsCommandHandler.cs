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

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreateManyPhotosToGoods
{
    internal class CreateManyPhotosToGoodsCommandHandler : IRequestHandler<CreateManyPhotosToGoodsCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateManyPhotosToGoodsCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateManyPhotosToGoodsCommand request, CancellationToken cancellationToken)
        {
            foreach (var photoToGood in request.PhotoToGoods)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == photoToGood.GoodId);

                if (good == null)
                {
                    throw new NotFoundException(nameof(Good), good.Id);
                }
            }

            foreach (var photoToGood in request.PhotoToGoods)
            {
                await _dbContext.PhotoToGoods.AddAsync(new PhotoToGood
                {
                    Id        = Guid.NewGuid(),
                    GoodId    = photoToGood.GoodId,
                    PhotoPath = photoToGood.PhotoPath,
                });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
