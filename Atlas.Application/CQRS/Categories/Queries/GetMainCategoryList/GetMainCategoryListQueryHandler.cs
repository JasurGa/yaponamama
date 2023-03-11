using Atlas.Application.Common.Helpers;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Categories.Queries.GetMainCategoryList
{
    public class GetMainCategoryListQueryHandler : IRequestHandler<GetMainCategoryListQuery, MainCategoryListVm>
    {
        private readonly IDriver _driver;
        private readonly IMapper _mapper;
        public GetMainCategoryListQueryHandler(IDriver driver, IMapper mapper) =>
            (_driver, _mapper) = (driver, mapper);

        public async Task<MainCategoryListVm> Handle(GetMainCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = new List<MainCategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c: Category { IsMainCategory: true, IsDeleted: $IsDeleted, IsHidden: $IsHidden }) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id: c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, NameRu: c.NameRu, NameEn: c.NameEn, NameUz: c.NameUz, ChildCategoriesCount: 0, GoodsCount: 0, OrderNumber: c.OrderNumber, IsHidden: c.IsHidden, IsVerified: c.IsVerified} AS r ORDER BY r.OrderNumber", new 
                { 
                    IsDeleted = request.ShowDeleted,
                    IsHidden  = request.ShowHidden,
                });

                categories = _mapper.Map<List<Category>, List<MainCategoryLookupDto>>(await cursor.ConvertDictManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            return new MainCategoryListVm { MainCategories = categories };
        }
    }
}
