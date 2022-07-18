using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommandHandler : IRequestHandler<DeleteCategoryToGoodCommand>
    {
        private readonly IDriver _driver;

        public DeleteCategoryToGoodCommandHandler(IDriver driver) =>
            _driver = driver;

        public async Task<Unit> Handle(DeleteCategoryToGoodCommand request, CancellationToken cancellationToken)
        {
            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (g:Good{Id: $GoodId})-[r:BELONGS_TO]->(c:Category{Id: $CategoryId}) DELETE r", new
                {
                    GoodId     = request.GoodId.ToString(),
                    CategoryId = request.CategoryId.ToString(),
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
