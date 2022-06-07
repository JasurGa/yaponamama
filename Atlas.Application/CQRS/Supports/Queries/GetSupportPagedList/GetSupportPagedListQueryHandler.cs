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

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList
{
    public class GetSupportPagedListQueryHandler : IRequestHandler<GetSupportPagedListQuery, PageDto<SupportLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetSupportPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<SupportLookupDto>> Handle(GetSupportPagedListQuery request, CancellationToken cancellationToken)
        {
            var supportsCount = await _dbContext.Supports.CountAsync(x =>
                x.IsDeleted == request.ShowDeleted, cancellationToken);

            var supports = await _dbContext.Supports
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<SupportLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<SupportLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = supportsCount,
                PageCount  = (int)Math.Ceiling((double)supportsCount / request.PageSize),
                Data       = supports,
            };
        }
    }
}
