using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildrenPagedList
{
    public class GetCategoryChildrenPagedListQueryHandler : IRequestHandler<GetCategoryChildrenPagedListQuery, PageDto<CategoryLookupDto>>
    {
        private readonly IDriver _driver;
        private readonly IMapper _mapper;

        public GetCategoryChildrenPagedListQueryHandler(IDriver driver, IMapper mapper) =>
            (_driver, _mapper) = (driver, mapper);

        public async Task<PageDto<CategoryLookupDto>> Handle(GetCategoryChildrenPagedListQuery request, CancellationToken cancellationToken)
        {
            int categoriesCount = 0;

            var categories = new List<CategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category {Id: $Id})<-[:BELONGS_TO]-(sc:Category{IsDeleted: $ShowDeleted}) RETURN COUNT(sc)", new 
                { 
                    Id          = request.Id.ToString(),
                    ShowDeleted = request.ShowDeleted
                });

                var record = await cursor.SingleAsync();
                categoriesCount = record[0].As<int>();

                cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted})-[:BELONGS_TO]->(p:Category{Id: $Id}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: $ShowDeleted}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id:c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, NameRu: c.NameRu, NameEn: c.NameEn, NameUz: c.NameUz, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g, OrderNumber: c.OrderNumber)} AS r ORDER BY r.OrderNumber SKIP $Skip LIMIT $Limit", new
                {
                    Id          = request.Id.ToString(),
                    ShowDeleted = request.ShowDeleted,
                    Skip        = request.PageIndex * request.PageSize,
                    Limit       = request.PageSize
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>
                    (await cursor.ConvertDictManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            return new PageDto<CategoryLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = categoriesCount,
                PageCount  = (int)Math.Ceiling((double)categoriesCount / request.PageSize),
                Data       = categories
            };
        }
    }
}
