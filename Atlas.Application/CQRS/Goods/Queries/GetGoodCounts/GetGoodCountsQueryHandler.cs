using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodCounts
{
    public class GetGoodCountsQueryHandler : IRequestHandler
        <GetGoodCountsQuery, int>
    {
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodCountsQueryHandler(IDriver driver, IAtlasDbContext dbContext) =>
            (_driver, _dbContext) = (driver, dbContext);

        public async Task<int> Handle(GetGoodCountsQuery request, CancellationToken cancellationToken)
        {
            var count = 0;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (g:Good)-[:BELONGS_TO]->(c:Category{Id: $Id}) RETURN COUNT(g)", new
                {
                    Id = request.CategoryId.ToString()
                });

                var record = await cursor.SingleAsync();
                count = record[0].As<int>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return count;
        }
    }
}
