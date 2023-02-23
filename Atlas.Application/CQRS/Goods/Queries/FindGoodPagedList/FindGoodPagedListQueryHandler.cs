using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Helpers;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Goods.Queries.FindGoodPagedList
{
    public class FindGoodPagedListQueryHandler : IRequestHandler<FindGoodPagedListQuery,
        PageDto<GoodLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public FindGoodPagedListQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<PageDto<GoodLookupDto>> Handle(FindGoodPagedListQuery request, CancellationToken cancellationToken)
        {
            var goods = _dbContext.Goods.AsQueryable();
            if (request.FilterCategoryId != null)
            {
                var goodIds = new List<Guid>();

                var session = _driver.AsyncSession();
                try
                {
                    var cursor = await session.RunAsync("MATCH (g:Good)-[:BELONGS_TO*]->(c:Category{Id: $CategoryId}) RETURN g.Id", new
                    {
                        CategoryId = request.FilterCategoryId.ToString()
                    });

                    var records = await cursor.ToListAsync();
                    foreach (var record in records)
                    {
                        goodIds.Add(Guid.Parse(record[0].As<string>()));
                    }
                }
                finally
                {
                    await session.CloseAsync();
                }

                goods = goods.Where(x => goodIds.Contains(x.Id));
            }
            if (request.FilterMinSellingPrice != null)
            {
                goods = goods.Where(x => x.SellingPrice >= request.FilterMinSellingPrice);
            }
            if (request.FilterMaxSellingPrice != null)
            {
                goods = goods.Where(x => x.SellingPrice <= request.FilterMaxSellingPrice);
            }

            goods = goods.Where(x => x.IsDeleted == request.ShowDeleted);

            if (request.SearchQuery != null)
            {
                var notTranslited = request.SearchQuery.ToLower().Trim();
                var translitedRu  = ""; // TranslitConverter.TranslitEnRu(notTranslited);
                var translitedEn  = ""; // TranslitConverter.TranslitRuEn(notTranslited);

                goods = goods.OrderBy(x => EF.Functions.TrigramsSimilarityDistance(
                    (x.NameRu + " " + x.NameEn + " " + x.NameUz + " " + x.SellingPrice + " " + x.PackageCode).ToLower().Trim(),
                        notTranslited + " " + translitedRu + " " + translitedEn));
            }

            var goodsCount = await goods.CountAsync(cancellationToken);
            var pagedGoods = await goods.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .Include(x => x.StoreToGoods)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = goodsCount,
                PageCount  = (int)Math.Ceiling((double)goodsCount / request.PageSize),
                Data       = pagedGoods,
            };
        }
    }
}

