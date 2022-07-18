using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Commands.AddCategoryParent
{
    public class AddCategoryParentCommandHandler : IRequestHandler<AddCategoryParentCommand>
    {
        private readonly IDriver _driver;

        public AddCategoryParentCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(AddCategoryParentCommand request,
            CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("MATCH (c:Category{Id: $Id}), (p:Category{Id: $ParentId}) " +
                    "CREATE (c)-[:BELONGS_TO]->(p)", new
                    {
                        Id       = request.CategoryId.ToString(),
                        ParentId = request.ParentId.ToString(),
                    });
            }
            finally
            {
                await session.CloseAsync();
            }

            return Unit.Value;
        }
    }
}
