using System.Threading;
using System.Threading.Tasks;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;
using Atlas.Application.Common.Helpers;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery,
        CategoryDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryDetailsQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryDetailsVm> Handle(GetCategoryDetailsQuery request,
            CancellationToken cancellationToken)
        {
            Category category;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{Id: $Id}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: false}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id:c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g)}", new
                {
                    Id = request.Id.ToString()
                });

                category = await cursor.ConvertAsync<Category>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return _mapper.Map<Category, CategoryDetailsVm>(category);
        }
    }
}
