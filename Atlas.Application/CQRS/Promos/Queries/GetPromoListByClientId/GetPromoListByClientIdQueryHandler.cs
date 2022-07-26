using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListByClientId
{
    public class GetPromoListByClientIdQueryHandler : IRequestHandler<GetPromoListByClientIdQuery, PromoListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoListByClientIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoListVm> Handle(GetPromoListByClientIdQuery request, CancellationToken cancellationToken)
        {
            //var promos = await _dbContext.Promos
            //    .Where(x => x.ClientId == request.ClientId)
            //    .ProjectTo<PromoLookupDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);

            //return new PromoListVm { Promos = promos };
            return new PromoListVm { Promos = null };
        }
    }
}
