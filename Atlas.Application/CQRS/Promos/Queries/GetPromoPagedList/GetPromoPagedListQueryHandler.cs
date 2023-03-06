using Atlas.Application.Common.Extensions;
using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
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
            var promosQuery = _dbContext.Promos.Select(x => x);

            if (request.FilterFromExpiresAt != null)
            {
                promosQuery = promosQuery.Where(x => 
                    x.ExpiresAt >= request.FilterFromExpiresAt);
            }
            if (request.FilterToExpiresAt != null)
            {
                promosQuery = promosQuery.Where(x =>
                    x.ExpiresAt <= request.FilterToExpiresAt);
            }

            var promosCount = await promosQuery.CountAsync(cancellationToken);
            
            var promos = await promosQuery
                .OrderByDynamic(request.Sortable, request.Ascending)
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
