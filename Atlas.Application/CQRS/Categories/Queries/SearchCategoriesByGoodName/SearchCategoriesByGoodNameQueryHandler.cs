using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchCategoriesByGoodNameQueryHandler : IRequestHandler<SearchCategoriesByGoodNameQuery,
        SearchedCategoryListVm>
    {
        private readonly IDriver         _driver;
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public SearchCategoriesByGoodNameQueryHandler(IDriver driver, IMapper mapper, IAtlasDbContext dbContext) =>
            (_driver, _mapper, _dbContext) = (driver, mapper, dbContext);

        public async Task<SearchedCategoryListVm> Handle(SearchCategoriesByGoodNameQuery request, CancellationToken cancellationToken)
        {
            var notTranslited = request.SearchQuery.ToLower().Trim();
            var translitedRu = TranslitConverter.TranslitEnRu(notTranslited);
            var translitedEn = TranslitConverter.TranslitRuEn(notTranslited);

            var goodIds = await _dbContext.Goods.Where(x => x.IsDeleted == false).OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance(
                (x.Name + " " + x.NameRu + " " + x.NameEn + " " + x.NameUz + " " + x.SellingPrice).ToLower().Trim(),
                       notTranslited + " " + translitedRu + " " + translitedEn))
                .Select(x => x.Id).Take(200)
                .ToListAsync(cancellationToken);

            List<SearchedCategoryLookupDto> categories;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category)<-[r:BELONGS_TO]-(g:Good) WHERE g.Id IN $Ids RETURN {Id: c.Id, Name: c.Name, NameRu: c.NameRu, NameEn: c.NameEn, NameUz: c.NameUz, Count: COUNT(r)};", new
                {
                    Ids = goodIds
                });

                categories = _mapper.Map<List<Category>, List<SearchedCategoryLookupDto>>(
                    await cursor.ConvertDictManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            return new SearchedCategoryListVm
            {
                Categories = categories.OrderBy(x => x.Count).ToList()
            };
        }
    }
}

