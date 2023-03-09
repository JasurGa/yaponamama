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
                NameRu         = request.NameRu,
                NameEn         = request.NameEn,
                NameUz         = request.NameUz,
                ImageUrl       = request.ImageUrl,
                IsMainCategory = request.IsMainCategory,
                OrderNumber    = request.OrderNumber,
                IsDeleted      = false,
                IsHidden       = request.IsHidden,
            };

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("CREATE (c:Category{Id: $Id, Name: $Name, NameRu: $NameRu, NameEn: $NameEn, NameUz: $NameUz, ImageUrl: $ImageUrl, IsMainCategory: $IsMainCategory, IsDeleted: False, OrderNumber: $OrderNumber, IsHidden: $IsHidden})", new
                {
                    Id             = category.Id.ToString(),
                    Name           = category.Name,
                    NameRu         = category.NameRu,
                    NameEn         = category.NameEn,
                    NameUz         = category.NameUz,
                    ImageUrl       = category.ImageUrl,
                    IsMainCategory = category.IsMainCategory,
                    OrderNumber    = category.OrderNumber,
                    IsHidden       = category.IsHidden,
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
