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
                ImageUrl       = request.ImageUrl,
                IsMainCategory = request.IsMainCategory,
            };

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("CREATE (c:Category{Id: $Id, Name: $Name, ImageUrl: $ImageUrl, IsMainCategory: $IsMainCategory, IsDeleted: False})", new
                {
                    Id             = category.Id.ToString(),
                    Name           = category.Name,
                    ImageUrl       = category.ImageUrl,
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
