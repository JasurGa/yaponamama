using System;
using MediatR;
using AutoMapper;
using Atlas.Domain;
using Neo4j.Driver;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryPagedList
{
    public class GetCategoryPagedListQueryHandler : IRequestHandler<GetCategoryPagedListQuery,
        PageDto<CategoryLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryPagedListQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<PageDto<CategoryLookupDto>> Handle(GetCategoryPagedListQuery request,
            CancellationToken cancellationToken)
        {
            int categoriesCount = 0;
            var categories = new List<CategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category { IsDeleted: $ShowDeleted, IsHidden: $ShowHidden, IsMainCategory: True}) RETURN COUNT(c)", new
                {
                    ShowDeleted = request.ShowDeleted,
                    ShowHidden  = request.ShowHidden,
                });

                var record = await cursor.SingleAsync();
                categoriesCount = record[0].As<int>();  

                cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted, IsHidden: $ShowHidden, IsMainCategory: true}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: $ShowDeleted, IsHidden: $ShowHidden}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id:c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, NameRu: c.NameRu, NameEn: c.NameEn, NameUz: c.NameUz, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g), OrderNumber: c.OrderNumber, IsHidden: c.IsHidden} AS r ORDER BY r[$Property] SKIP $Skip LIMIT $Limit", new
                {
                    ShowDeleted = request.ShowDeleted,
                    ShowHidden  = request.ShowHidden,
                    Skip        = request.PageIndex * request.PageSize,
                    Limit       = request.PageSize,
                    Property    = request.Sortable,
                    Ascending   = request.Ascending
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertDictManyAsync<Category>());
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