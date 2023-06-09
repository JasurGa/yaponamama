﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.Goods.Queries.GetTopGoods;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Neo4j.Driver;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories
{
    public class GetGoodsForMainCategoriesQueryHandler : IRequestHandler
        <GetGoodsForMainCategoriesQuery, TopGoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodsForMainCategoriesQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<TopGoodListVm> Handle(GetGoodsForMainCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<TopGoodDetailsVm>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsMainCategory: True, IsDeleted: False}) RETURN c");

                var categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertManyAsync<Category>());

                foreach (var c in categories)
                {
                    var goodIds = new List<Guid>();

                    cursor = await session.RunAsync("MATCH (m:Category{Id: $Id})<-[:BELONGS_TO]-(c:Category)<-[:BELONGS_TO]-(g:Good) RETURN rand() as r, g.Id ORDER BY r LIMIT 10", new
                    {
                        Id = c.Id.ToString()
                    });

                    var records = await cursor.ToListAsync();
                    foreach (var record in records)
                    {                                                                                                                                                 
                        goodIds.Add(Guid.Parse(record[1].As<string>()));
                    }

                    cursor = await session.RunAsync("MATCH (g:Good)-[r:BELONGS_TO*..]->(c:Category) WHERE g.Id IN $GoodIds RETURN {GoodId: g.Id, CategoryIds: COLLECT(DISTINCT c.Id)}", new
                    {
                        GoodIds = goodIds.Select(x => x.ToString())
                    });

                    var goodIdsWithCategories = await cursor.ConvertDictManyAsync<GoodToCategoriesLookupDto>();

                    var goods = await _dbContext.Goods.OrderBy(x => x.NameRu)
                        .OrderByDescending(x => x.StoreToGoods.Select(x => x.Count).Sum())
                        .Where(x => goodIds.Contains(x.Id) && x.IsDeleted == request.ShowDeleted)
                        .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    result.Add(new TopGoodDetailsVm
                    {
                        Category = c,
                        Goods    = goods.Select((x) =>
                        {
                            var categories = goodIdsWithCategories.FirstOrDefault(y => y.GoodId == x.Id);
                            if (categories != null)
                            {
                                x.Categories = categories.CategoryIds;
                            }

                            return x;
                        }).ToList()
                    });
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return new TopGoodListVm
            {
                Categories = result
            };
        }
    }
}
