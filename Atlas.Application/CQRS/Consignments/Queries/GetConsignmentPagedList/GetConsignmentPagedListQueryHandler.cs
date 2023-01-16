using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
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

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentPagedList
{
    public class GetConsignmentPagedListQueryHandler : IRequestHandler<GetConsignmentPagedListQuery, PageDto<ConsignmentLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetConsignmentPagedListQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<PageDto<ConsignmentLookupDto>> Handle(GetConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            var consignmentsQuery = _dbContext.Consignments
                .OrderByDynamic(request.Sortable, request.Ascending)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ConsignmentLookupDto>(_mapper.ConfigurationProvider);

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

                consignmentsQuery = consignmentsQuery.Where(x => goodIds.Contains(x.GoodId));
            }

            var consignmentsCount = await consignmentsQuery.CountAsync(cancellationToken);
            var consignments = await consignmentsQuery.ToListAsync(cancellationToken);

            return new PageDto<ConsignmentLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = consignmentsCount,
                PageCount  = (int)Math.Ceiling((double)consignmentsCount / request.PageSize),
                Data       = consignments,
            };
        }
    }
}
