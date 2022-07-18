using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Commands.RemoveCategoryParent
{
    public class RemoveCategoryParentCommandHandler : IRequestHandler<RemoveCategoryParentCommand>
    {
        private readonly IDriver _driver;

        public RemoveCategoryParentCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(RemoveCategoryParentCommand request,
            CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("MATCH (c:Category{Id: $Id})-[r:BELONGS_TO]->(p:Category{Id: $ParentId}) " +
                    "DELETE r", new
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
