using System;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Extensions;
using AutoMapper.QueryableExtensions;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByPromoCategory
{
    public class GetGoodPagedListByPromoCategoryQueryHandler : IRequestHandler<GetGoodPagedListByPromoCategoryQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodPagedListByPromoCategoryQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodPagedListByPromoCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var goodIds = await _dbContext.PromoCategoryToGoods.Where(x =>
                    x.PromoCategoryId == request.PromoCategoryId)
                .Select(x => x.GoodId)
                .ToListAsync(cancellationToken);

            var goodsCount = await _dbContext.Goods
                .CountAsync(x => goodIds.Contains(x.Id) &&
                    x.IsDeleted == request.ShowDeleted,
                        cancellationToken);

            var goods = await _dbContext.Goods
                .Where(x => goodIds.Contains(x.Id) &&
                    x.IsDeleted == request.ShowDeleted)
                .OrderByDynamic(request.Sortable, request.Ascending)
                .OrderByDescending(x => x.StoreToGoods.Select(x => x.Count).Sum())
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodsCount,
                PageCount  = (int)Math.Ceiling((double)goodsCount/request.PageSize),
                Data       = goods
            };
        }
    }
}

