using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Domain;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IDriver _driver;

        public CreateCategoryCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id             = Guid.NewGuid(),
                Name           = request.Name,
                IsMainCategory = request.IsMainCategory,
            };

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("CREATE (c:Category{Id: $Id, Name: $Name, IsMainCategory: $IsMainCategory, IsDeleted: False})", new
                {
                    Id             = category.Id.ToString(),
                    Name           = category.Name.ToString(),
                    IsMainCategory = category.IsMainCategory
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            return category.Id;
        }
    }
}
