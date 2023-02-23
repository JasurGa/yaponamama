using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Consignments.Queries.FindConsignmentsPagedList
{
    public class FindConsignmentPagedListQueryHandler : IRequestHandler<FindConsignmentPagedListQuery,
        PageDto<ConsignmentLookupDto>>
    {
        private readonly IMapper         _mapper; 
        private readonly IAtlasDbContext _dbContext;

        public FindConsignmentPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ConsignmentLookupDto>> Handle(FindConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Consignments
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .AsQueryable();

            if (request.FilterStartDate != null && request.FilterEndDate != null)
            {
                query = query.Where(x => x.PurchasedAt >= request.FilterStartDate && x.PurchasedAt <= request.FilterEndDate);
            }

            if (request.SearchQuery != null)
            {
                query = query.OrderBy(x =>
                    EF.Functions.TrigramsStrictWordSimilarityDistance((x.StoreToGood.Good.NameRu + " " + x.StoreToGood.Good.NameEn + " " + x.StoreToGood.Good.NameUz).ToLower().Trim(),
                        request.SearchQuery.ToLower().Trim()));
            }
            else
            {
                query = query.OrderByDynamic(request.Sortable, request.Ascending);
            }

            var consignmentsCount = await query.CountAsync(cancellationToken);
            var consignmnets = await query
                .Include(x => x.StoreToGood.Good)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<ConsignmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ConsignmentLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = consignmentsCount,
                PageCount  = (int)Math.Ceiling((double)consignmentsCount / request.PageSize),
                Data       = consignmnets,
            };
        }
    }
}

