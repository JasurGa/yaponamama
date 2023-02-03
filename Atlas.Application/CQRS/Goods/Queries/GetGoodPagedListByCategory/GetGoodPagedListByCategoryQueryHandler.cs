using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Extensions;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedListByCategory
{
    public class GetGoodPagedListByCategoryQueryHandler : IRequestHandler<GetGoodPagedListByCategoryQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodPagedListByCategoryQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(GetGoodPagedListByCategoryQuery request,
            CancellationToken cancellationToken)
        {
            var goodIds = new List<Guid>();
            var goodIdsWithCategories = new List<GoodToCategoriesLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (g:Good)-[:BELONGS_TO*]->(c:Category{Id: $CategoryId}) RETURN g.Id", new
                {
                    CategoryId = request.CategoryId.ToString()
                });

                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    goodIds.Add(Guid.Parse(record[0].As<string>()));
                }

                cursor = await session.RunAsync("MATCH (g:Good)-[r:BELONGS_TO*..]->(c:Category) WHERE g.Id IN $GoodIds RETURN {GoodId: g.Id, CategoryIds: COLLECT(DISTINCT c.Id)}", new
                {
                    GoodIds = goodIds.Select(x => x.ToString())
                });

                goodIdsWithCategories = await cursor.ConvertDictManyAsync<GoodToCategoriesLookupDto>();
            }
            finally
            {
                await session.CloseAsync();
            }

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
                PageCount  = (int)Math.Ceiling((double)goodsCount /
                    request.PageSize),
                Data       = goods.Select((x) =>
                {
                    var categories = goodIdsWithCategories.FirstOrDefault(y => y.GoodId == x.Id);
                    if (categories != null)
                    {
                        x.Categories = categories.CategoryIds;
                    }

                    return x;
                }).ToList()
            };
        }
    }
}
