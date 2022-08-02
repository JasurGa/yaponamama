using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Statistics.Queries.GetGoodsCountStatistics
{
    public class CategoryCount
    {
        public Guid CategoryId { get; set; }

        public int Count { get; set; }
    }

    public class GetGoodsCountStatisticsQueryHandler : IRequestHandler
        <GetGoodsCountStatisticsQuery, GoodsCountStatisticsVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetGoodsCountStatisticsQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<GoodsCountStatisticsVm> Handle(GetGoodsCountStatisticsQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<GoodsCountLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: False})<-[*]-(g:Good) RETURN c.Id AS CategoryId, COUNT(g) AS Count");

                var categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertManyAsync<Category>());

                cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: False})<-[*]-(g:Good) RETURN c.Id, COUNT(g)");

                var categoryCounts = await cursor.ConvertManyAsync<CategoryCount>();

                foreach (var c in categories)
                {
                    var goodCount = categoryCounts.Find(x => x.CategoryId == c.Id);

                    result.Add(new GoodsCountLookupDto
                    {
                        Category  = c,
                        GoodCount = goodCount == null ? goodCount.Count : 0
                    });
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return new GoodsCountStatisticsVm
            {
                Categories = result
            };
        }
    }
}
