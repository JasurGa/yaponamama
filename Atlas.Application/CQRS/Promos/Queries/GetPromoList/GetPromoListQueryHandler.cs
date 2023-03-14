using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class GetPromoListQueryHandler : IRequestHandler<GetPromoListQuery, PromoListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) => 
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoListVm> Handle(GetPromoListQuery request, CancellationToken cancellationToken)
        {
            var promos = await _dbContext.Promos
                .Include(x => x.Client).ThenInclude(x => x.User)
                .ProjectTo<PromoLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoListVm { Promos = promos };
        }
    }
}
