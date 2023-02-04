using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionPagedList
{
    public class GetCorrectionPagedListQueryHandler : IRequestHandler<GetCorrectionPagedListQuery, PageDto<CorrectionLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetCorrectionPagedListQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<PageDto<CorrectionLookupDto>> Handle(GetCorrectionPagedListQuery request, CancellationToken cancellationToken)
        {
            var correctionsQuery = _dbContext.Corrections
                .OrderByDynamic(request.Sortable, request.Ascending)
                .ProjectTo<CorrectionLookupDto>(_mapper.ConfigurationProvider);

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

                correctionsQuery = correctionsQuery.Where(x => goodIds.Contains(x.Good.Id));
            }

            var correctionsCount = await correctionsQuery
                .CountAsync(cancellationToken);

            var corrections = await correctionsQuery
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PageDto<CorrectionLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = correctionsCount,
                PageCount  = (int)Math.Ceiling((double)correctionsCount / request.PageSize),
                Data       = corrections,
            };
        }
    }
}
