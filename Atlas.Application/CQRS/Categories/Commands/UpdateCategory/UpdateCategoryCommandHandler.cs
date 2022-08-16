using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IDriver _driver;

        public UpdateCategoryCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("MATCH (c:Category{Id: $Id}) SET c.Name = $Name, NameRu = $NameRu, NameEn = $NameEn, NameUz = $NameUz, c.ImageUrl = $ImageUrl, c.IsMainCategory = $IsMainCategory", new
                {
                    Id             = request.Id.ToString(),
                    Name           = request.Name,
                    NameRu         = request.NameRu,
                    NameEn         = request.NameEn,
                    NameUz         = request.NameUz,
                    ImageUrl       = request.ImageUrl,
                    IsMainCategory = request.IsMainCategory
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
