using Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList;
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

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentPagedList
{
    public class GetConsignmentPagedListQueryHandler : IRequestHandler<GetConsignmentPagedListQuery, PageDto<ConsignmentLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetConsignmentPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ConsignmentLookupDto>> Handle(GetConsignmentPagedListQuery request, CancellationToken cancellationToken)
        {
            var consignmentsCount = await _dbContext.Consignments.CountAsync(cancellationToken);

            var consignments = await _dbContext.Consignments
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ConsignmentLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

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
