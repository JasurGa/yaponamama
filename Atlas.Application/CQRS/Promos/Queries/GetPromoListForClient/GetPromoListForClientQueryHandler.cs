using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Interfaces;
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

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListForClient
{
    public class GetPromoListForClientQueryHandler : IRequestHandler<GetPromoListForClientQuery, PromoListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoListForClientQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoListVm> Handle(GetPromoListForClientQuery request, CancellationToken cancellationToken)
        {
            var promos = await _dbContext.Promos
                .Where(x => x.ClientId == request.ClientId)
                .ProjectTo<PromoLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoListVm { Promos = promos };
        }
    }
}
