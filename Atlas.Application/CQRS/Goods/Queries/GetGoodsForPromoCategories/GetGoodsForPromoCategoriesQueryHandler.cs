using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetTopGoods;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForPromoCategories
{
    public class GetGoodsForPromoCategoriesQueryHandler : IRequestHandler
        <GetGoodsForPromoCategoriesQuery, PromoTopGoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodsForPromoCategoriesQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoTopGoodListVm> Handle(GetGoodsForPromoCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<PromoTopGoodDetailsVm>();

            var promoCategories = await _dbContext.PromoCategories.Where(x => !x.IsDeleted)
                .ProjectTo<PromoCategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var promoCategory in promoCategories)
            {
                var goodIds = await _dbContext.PromoCategoryToGoods
                    .Where(x => x.PromoCategoryId == promoCategory.Id)
                    .Select(x => x.GoodId)
                    .ToListAsync(cancellationToken);

                var goods = await _dbContext.Goods.OrderBy(x => x.NameRu)
                    .OrderByDescending(x => x.StoreToGoods.Select(x => x.Count).Sum())
                    .Where(x => goodIds.Contains(x.Id) && x.IsDeleted == request.ShowDeleted)
                    .Take(4)
                    .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                result.Add(new PromoTopGoodDetailsVm
                {
                    PromoCategory = promoCategory,
                    Goods         = goods
                });
            }

            return new PromoTopGoodListVm
            {
                PromoCategories = result
            };
        }
    }
}

