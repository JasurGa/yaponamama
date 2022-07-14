﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Helpers;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildren
{
    public class GetCategoryChildrenQueryHandler : IRequestHandler
        <GetCategoryChildrenQuery, CategoryListVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryChildrenQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryListVm> Handle(GetCategoryChildrenQuery request,
            CancellationToken cancellationToken)
        {
            List<CategoryLookupDto> categories;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: $ShowDeleted})-[:BELONGS_TO]->(p:Category{Id: $Id}) RETURN c", new
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