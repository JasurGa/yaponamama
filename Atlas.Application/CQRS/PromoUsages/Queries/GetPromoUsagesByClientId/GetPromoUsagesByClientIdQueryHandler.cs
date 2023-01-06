using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId
{
    public class GetPromoUsagesByClientIdQueryHandler : IRequestHandler<GetPromoUsagesByClientIdQuery,
        PromoUsageListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoUsagesByClientIdQueryHandler(IAtlasDbContext dbContext, IMapper mapper) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoUsageListVm> Handle(GetPromoUsagesByClientIdQuery request, CancellationToken cancellationToken)
        {
            var promoUsages = await _dbContext.PromoUsages.Where(x =>
                x.ClientId == request.ClientId)
                .ProjectTo<PromoUsageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoUsageListVm
            {
                PromoUsages = promoUsages
            };
        }
    }
}

