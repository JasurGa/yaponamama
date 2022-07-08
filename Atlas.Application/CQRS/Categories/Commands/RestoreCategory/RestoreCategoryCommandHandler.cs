using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommandHandler : IRequestHandler<RestoreCategoryCommand>
    {
        private readonly IDriver _driver;

        public RestoreCategoryCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                await session.RunAsync("MATCH (c:Category{Id: $Id}) SET c.IsDeleted = False",
                    new { Id = request.Id.ToString() });
            }
            finally
            {
                await session.CloseAsync();
            }

            return Unit.Value;
        }
    }
}
