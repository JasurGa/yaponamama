using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryList
{
    public class ChildToParent
    {
        public Guid ChildId { get; set; }

        public Guid ParentId { get; set; }
    }

    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery,
        CategoryListVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryListQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryListVm> Handle(GetCategoryListQuery request,
            CancellationToken cancellationToken)
        {
            List<CategoryLookupDto> categories;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $IsDeleted}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: $IsDeleted}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id: c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g)} AS r", new
                {
                    IsDeleted = request.ShowDeleted
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertDictManyAsync<Category>());

                cursor = await session.RunAsync("MATCH (p:Category{IsDeleted: $IsDeleted})<-[:BELONGS_TO]-(c:Category{IsDeleted: $IsDeleted}) RETURN p.Id AS ParentId, c.Id As ChildId", new
                {
                    IsDeleted = request.ShowDeleted
                });

                var records = await cursor.ToListAsync();

                var childrenToCategories = new List<ChildToParent>();
                foreach (var record in records)
                {
                    childrenToCategories.Add(new ChildToParent
                    {
                        ParentId = Guid.Parse(record[0].As<string>()),
                        ChildId  = Guid.Parse(record[1].As<string>())
                    });
                }

                categories.ForEach((e) =>
                {
                    e.Children = childrenToCategories.Where(x => x.ParentId == e.Id)
                        .Select(x => x.ChildId).ToList();
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            return new CategoryListVm { Categories = categories };
        }
    }
}
