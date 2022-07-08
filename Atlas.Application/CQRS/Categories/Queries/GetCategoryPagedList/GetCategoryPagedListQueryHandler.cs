using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Helpers;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

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
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted}) RETURN COUNT(c)", new
                {
                    ShowDeleted = request.ShowDeleted.ToString().ToLower()
                });

                var record = await cursor.SingleAsync();
                categoriesCount = record[0].As<int>();

                cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted}) RETURN c SKIP $Skip LIMIT $Limit", new
                {
                    ShowDeleted = request.ShowDeleted.ToString().ToLower(),
                    Skip        = request.PageIndex * request.PageSize,
                    Limit       = request.PageSize
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertManyAsync<Category>());
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
