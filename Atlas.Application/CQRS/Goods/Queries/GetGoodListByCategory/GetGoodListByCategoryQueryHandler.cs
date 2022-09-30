using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQueryHandler : IRequestHandler<GetGoodListByCategoryQuery,
        GoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodListByCategoryQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<GoodListVm> Handle(GetGoodListByCategoryQuery request,
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

            var goods = await _dbContext.Goods.OrderBy(x => x.NameRu)
                .Where(x => goodIds.Contains(x.Id) && x.IsDeleted == request.ShowDeleted)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm
            {
                Goods = goods.Select((x) =>
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
