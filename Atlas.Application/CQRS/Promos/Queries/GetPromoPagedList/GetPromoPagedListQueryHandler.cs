using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList
{
    public class GetPromoPagedListQueryHandler : IRequestHandler<GetPromoPagedListQuery, PageDto<PromoLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<PromoLookupDto>> Handle(GetPromoPagedListQuery request, CancellationToken cancellationToken)
        {
            var promosCount = await _dbContext.Promos.CountAsync(cancellationToken);
            
            var promos = await _dbContext.Promos
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<PromoLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<PromoLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = promosCount,
                PageCount  = (int)Math.Ceiling((double)promosCount / request.PageSize),
                Data       = promos
            };
        }
    }
}
