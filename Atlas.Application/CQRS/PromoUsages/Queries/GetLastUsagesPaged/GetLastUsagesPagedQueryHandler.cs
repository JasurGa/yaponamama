using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.PromoUsages.Queries.GetByClientId;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetLastUsagesPaged
{
    public class GetLastUsagesPagedQueryHandler : IRequestHandler<GetLastUsagesPagedQuery,
        PageDto<PromoUsageLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLastUsagesPagedQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<PromoUsageLookupDto>> Handle(GetLastUsagesPagedQuery request, CancellationToken cancellationToken)
        {
            var promoUsagesCount = await _dbContext.PromoUsages
                .CountAsync(cancellationToken);

            var promoUsages = await _dbContext.PromoUsages
                .OrderBy(x => x.UsedAt)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<PromoUsageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<PromoUsageLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = promoUsagesCount,
                PageCount  = (int)Math.Ceiling((double)promoUsagesCount / request.PageSize),
                Data       = promoUsages
            };
        }
    }
}

