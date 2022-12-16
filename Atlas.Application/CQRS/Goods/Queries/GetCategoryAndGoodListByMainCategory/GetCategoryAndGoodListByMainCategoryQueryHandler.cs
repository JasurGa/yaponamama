using Atlas.Application.Common.Helpers;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class GetCategoryAndGoodListByMainCategoryQueryHandler : IRequestHandler<GetCategoryAndGoodListByMainCategoryQuery, CategoryAndGoodListVm>
    {
        private readonly IDriver _driver;
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCategoryAndGoodListByMainCategoryQueryHandler(IDriver driver, IMapper mapper, IAtlasDbContext dbContext) =>
            (_driver, _mapper, _dbContext) = (driver, mapper, dbContext);

        public async Task<CategoryAndGoodListVm> Handle(GetCategoryAndGoodListByMainCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = new List<CategoryWithGoodsLookupDto>();

            var session = _driver.AsyncSession();

            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category {Id: $Id, IsMainCategory: true})<-[:BELONGS_TO]-(sc:Category) RETURN sc", new
                {
                    Id = request.MainCategoryId.ToString()
                });

                var categories = _mapper.Map<List<Category>, List<CategoryWithGoodsLookupDto>>(await cursor.ConvertManyAsync<Category>());

                foreach (var category in categories)
                {
                    var goodIds = new List<Guid>();

                    cursor = await session.RunAsync("MATCH (c:Category {Id: $Id})<-[:BELONGS_TO]-(g:Good) RETURN g.Id LIMIT 15", new {
                        Id = category.Id.ToString()
                    });

                    var records = await cursor.ToListAsync();
                    foreach (var record in records)
                    {
                        goodIds.Add(Guid.Parse(record[1].As<string>()));
                    }

                    var goodsCount = await _dbContext.Goods.CountAsync(x => goodIds.Contains(x.Id) && x.IsDeleted == false, 
                            cancellationToken);

                    var goods = await _dbContext.Goods
                        .Where(x => goodIds.Contains(x.Id) && x.IsDeleted == false)
                        .ProjectTo<GoodInCategoryLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    result.Add(new CategoryWithGoodsLookupDto { 
                        Id         = category.Id,
                        Name       = category.Name,
                        GoodsCount = goodsCount,
                        Goods      = goods
                    });
                }
            }
            finally
            {
                await session.CloseAsync();
            }


            return new CategoryAndGoodListVm
            {
                Categories = result
            };
        }
    }
}
