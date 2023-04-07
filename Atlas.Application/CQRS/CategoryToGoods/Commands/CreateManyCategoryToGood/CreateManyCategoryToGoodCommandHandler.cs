using Atlas.Application.Interfaces;
using MediatR;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateManyCategoryToGood
{
    public class CreateManyCategoryToGoodCommandHandler : IRequestHandler<CreateManyCategoryToGoodCommand>
    {
        private readonly IDriver _driver;

        public CreateManyCategoryToGoodCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(CreateManyCategoryToGoodCommand request, CancellationToken cancellationToken)
        {
            foreach (var categoryToGood in request.CategoriesToGoods)
            {
                var session = _driver.AsyncSession();
                try
                {
                    try
                    {
                        var cursor = await session.RunAsync("MATCH (c:Category{Id: $CategoryId}), (g:Good{Id: $GoodId}) CREATE (g)-[:BELONGS_TO]->(c) RETURN g", new
                        {
                            GoodId     = categoryToGood.GoodId.ToString(),
                            CategoryId = categoryToGood.CategoryId.ToString(),
                        });

                        await cursor.SingleAsync();
                    }
                    catch
                    {
                        await session.RunAsync("MATCH (c:Category{Id: $CategoryId}) CREATE (g:Good{Id:$GoodId}), (g)-[:BELONGS_TO]->(c)", new
                        {
                            GoodId     = categoryToGood.GoodId.ToString(),
                            CategoryId = categoryToGood.CategoryId.ToString(),
                        });
                    }
                }
                finally
                {
                    await session.CloseAsync();
                }
            }

            return Unit.Value;
        }
    }
}
