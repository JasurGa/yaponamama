using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood
{
    public class CreateCategoryToGoodCommandHandler : IRequestHandler<CreateCategoryToGoodCommand>
    {
        private readonly IDriver _driver;

        public CreateCategoryToGoodCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(CreateCategoryToGoodCommand request,
            CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                try
                {
                    var cursor = await session.RunAsync("MATCH (c:Category{Id: $CategoryId}), (g:Good{Id: $GoodId}) CREATE (g)-[:BELONGS_TO]->(c) RETURN g", new
                    {
                        GoodId     = request.GoodId.ToString(),
                        CategoryId = request.CategoryId.ToString(),
                    });

                    await cursor.SingleAsync();
                }
                catch
                {
                    await session.RunAsync("MATCH (c:Category{Id: $CategoryId}) CREATE (g:Good{Id:$GoodId}), (g)-[:BELONGS_TO]->(c)", new
                    {
                        GoodId     = request.GoodId.ToString(),
                        CategoryId = request.CategoryId.ToString(),
                    });
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return Unit.Value;
        }
    }
}
