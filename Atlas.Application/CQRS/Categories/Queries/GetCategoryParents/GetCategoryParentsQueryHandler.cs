using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Common.Helpers;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryParents
{
    public class GetCategoryParentsQueryHandler : IRequestHandler
        <GetCategoryParentsQuery, CategoryListVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryParentsQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryListVm> Handle(GetCategoryParentsQuery request,
            CancellationToken cancellationToken)
        {
            List<CategoryLookupDto> categories;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted})<-[:BELONGS_TO]-(p:Category{Id: $Id}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: $ShowDeleted}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id:c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g)}", new
                {
                    Id          = request.Id.ToString(),
                    ShowDeleted = request.ShowDeleted,
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
