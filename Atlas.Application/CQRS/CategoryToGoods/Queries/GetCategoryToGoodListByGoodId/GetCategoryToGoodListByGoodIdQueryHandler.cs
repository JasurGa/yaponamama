using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Helpers;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class GetCategoryToGoodListByGoodIdQueryHandler : IRequestHandler
        <GetCategoryToGoodListByGoodIdQuery, CategoryListVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryToGoodListByGoodIdQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryListVm> Handle(GetCategoryToGoodListByGoodIdQuery request,
            CancellationToken cancellationToken)
        {
            var categories = new List<CategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (g:Good{Id: $Id})-[:BELONGS_TO]->(c:Category{IsDeleted: $IsDeleted}) RETURN c", new
                {
                    Id          = request.GoodId.ToString(),
                    ShowDeleted = request.ShowDeleted
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            return new CategoryListVm { Categories = categories };
        }
    }
}
