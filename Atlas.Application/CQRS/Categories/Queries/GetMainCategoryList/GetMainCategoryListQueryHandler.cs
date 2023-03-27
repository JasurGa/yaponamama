using Atlas.Application.Common.Helpers;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList
{
    public class GetMainCategoryListQueryHandler : IRequestHandler<GetMainCategoryListQuery, MainCategoryListVm>
    {
        private readonly IDriver         _driver;
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetMainCategoryListQueryHandler(IDriver driver, IMapper mapper, IAtlasDbContext dbContext) =>
            (_driver, _mapper, _dbContext) = (driver, mapper, dbContext);

        public async Task<MainCategoryListVm> Handle(GetMainCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = new List<MainCategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c: Category { IsMainCategory: true, IsDeleted: $IsDeleted, IsHidden: $IsHidden }) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id: c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, NameRu: c.NameRu, NameEn: c.NameEn, NameUz: c.NameUz, ChildCategoriesCount: 0, GoodsCount: 0, OrderNumber: c.OrderNumber, IsHidden: c.IsHidden, IsVerified: c.IsVerified} AS r ORDER BY r.OrderNumber", new 
                { 
                    IsDeleted = request.ShowDeleted,
                    IsHidden  = request.ShowHidden
                });

                categories = _mapper.Map<List<Category>, List<MainCategoryLookupDto>>(await cursor.ConvertDictManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            if (!request.IsAuthenticated || request.ClientId != Guid.Empty)
            {
                var client = await _dbContext.Clients.FirstOrDefaultAsync(x =>
                    x.Id == request.ClientId, cancellationToken);

                if (client == null || !client.IsPassportVerified)
                {
                    categories = categories.Where(x => !x.IsVerified).ToList();
                }
            }

            return new MainCategoryListVm { MainCategories = categories };
        }
    }
}
