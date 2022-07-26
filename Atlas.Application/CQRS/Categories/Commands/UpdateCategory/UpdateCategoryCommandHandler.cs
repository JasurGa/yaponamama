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
                await session.RunAsync("MATCH (c:Category{Id: $Id}) SET c.Name = $Name, c.ImageUrl = $ImageUrl, c.IsMainCategory = $IsMainCategory", new
                {
                    Id             = request.Id,
                    Name           = request.Name,
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
