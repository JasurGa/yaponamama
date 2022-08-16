using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
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

namespace Atlas.Application.CQRS.Goods.Queries.GetDiscountedGoodListByCategory
{
    public class GetDiscountedGoodListByCategoryQueryHandler : IRequestHandler<GetDiscountedGoodListByCategoryQuery, GoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetDiscountedGoodListByCategoryQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<GoodListVm> Handle(GetDiscountedGoodListByCategoryQuery request, CancellationToken cancellationToken)
        {
            var goodsIds = new List<Guid>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (g:Good)-[:BELONGS_TO]->(c:Category{Id: $Id}) RETURN g.Id", new
                {
                    Id = request.CategoryId.ToString()
                });

                var records = await cursor.ToListAsync();
                foreach (var record in records)
                {
                    goodsIds.Add(Guid.Parse(record[0].As<string>()));
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            var goods = await _dbContext.Goods
                .Where(x => goodsIds.Contains(x.Id) & x.IsDeleted == false & x.Discount != 0)
                .Take(10)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
