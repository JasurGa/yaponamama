using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart
{
	public class UpdateManyGoodsInCartCommandHandler : IRequestHandler<UpdateManyGoodsInCartCommand,
		LackingGoodListVm>
	{
		private readonly IMapper _mapper;

		private readonly IAtlasDbContext _dbContext;

		public UpdateManyGoodsInCartCommandHandler(IMapper mapper, IAtlasDbContext dbContext) =>
			(_mapper, _dbContext) = (mapper, dbContext);

        public async Task<LackingGoodListVm> Handle(UpdateManyGoodsInCartCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                x.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            foreach (var goodToCount in request.GoodsToCount)
            {
                var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                    x.Id == goodToCount.Key, cancellationToken);

                if (good == null)
                {
                    throw new NotFoundException(nameof(Good), goodToCount.Key);
                }
            }

            _dbContext.GoodToCarts.RemoveRange(
                _dbContext.GoodToCarts.Where(x =>
                    x.ClientId == request.ClientId));
            await _dbContext.SaveChangesAsync(cancellationToken);

            foreach (var goodToCount in request.GoodsToCount)
            {
                await _dbContext.GoodToCarts.AddAsync(new GoodToCart
                {
                    Id       = Guid.NewGuid(),
                    ClientId = request.ClientId,
                    GoodId   = goodToCount.Key,
                    Count    = goodToCount.Value,
                },
                cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = new LackingGoodListVm
            {
                LackingGoods = new List<LackingGoodLookupDto>()
            };

            var stores = await _dbContext.Stores
                .ToListAsync(cancellationToken);

            foreach (var store in stores)
            {
                foreach (var goodToCount in request.GoodsToCount)
                {
                    var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                        x.StoreId == store.Id && x.GoodId == goodToCount.Key);

                    if (storeToGood != null && storeToGood.Count < goodToCount.Value)
                    {
                        result.LackingGoods.Add(new LackingGoodLookupDto
                        {
                            GoodId   = goodToCount.Key,
                            StoreId  = store.Id,
                            MaxCount = storeToGood.Count,
                        });
                    }
                }
            }

            return result;
        }
    }
}

