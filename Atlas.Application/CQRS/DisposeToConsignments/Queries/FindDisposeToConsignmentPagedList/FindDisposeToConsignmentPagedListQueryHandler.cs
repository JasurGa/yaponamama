using Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.FindDisposeToConsignmentPagedList
{
    public class FindDisposeToConsignmentPagedListQueryHandler : IRequestHandler<FindDisposeToConsignmentPagedListQuery, PageDto<DisposeToConsignmentLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindDisposeToConsignmentPagedListQueryHandler(IMapper mapper,IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<DisposeToConsignmentLookupDto>> Handle(FindDisposeToConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.DisposeToConsignments.AsQueryable();

            if (request.SearchQuery != null)
            {
                query = query.OrderByDescending(x => 
                    EF.Functions.TrigramsSimilarity((x.Id + " " + x.Consignment.StoreToGood.Good.Name + " " + x.Consignment.StoreToGood.Good.NameRu + " " + x.Consignment.StoreToGood.Good.NameEn + " " + x.Consignment.StoreToGood.Good.NameUz + " " + x.Comment).ToLower().Trim(), 
                        request.SearchQuery.ToLower().Trim()));
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedAt);
            }

            var disposeToConsignmentsCount = await query.CountAsync(cancellationToken);
            var disposeToConsignments = await query
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<DisposeToConsignmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<DisposeToConsignmentLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = disposeToConsignmentsCount,
                PageCount = (int)Math.Ceiling((double)disposeToConsignmentsCount / request.PageSize),
                Data = disposeToConsignments,
            };
        }
    }
}
