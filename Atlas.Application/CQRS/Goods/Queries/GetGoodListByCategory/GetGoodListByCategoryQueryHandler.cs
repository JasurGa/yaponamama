using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            }
            finally
            {
                await session.CloseAsync();
            }

            var goods = await _dbContext.Goods
                .Where(x => goodIds.Contains(x.Id) && x.IsDeleted == request.ShowDeleted)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
