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

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList
{
    public class GetDisposeToConsignmentPagedListQueryHandler : IRequestHandler<GetDisposeToConsignmentPagedListQuery, PageDto<DisposeToConsignmentLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetDisposeToConsignmentPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<DisposeToConsignmentLookupDto>> Handle(GetDisposeToConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            var disposeToConsignmentQuery = _dbContext.DisposeToConsignments
                .ProjectTo<DisposeToConsignmentLookupDto>(_mapper.ConfigurationProvider);

            var disposeToConsignmentsCount = await disposeToConsignmentQuery
                .CountAsync(cancellationToken);

            var disposeToConsignments = await disposeToConsignmentQuery
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
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
