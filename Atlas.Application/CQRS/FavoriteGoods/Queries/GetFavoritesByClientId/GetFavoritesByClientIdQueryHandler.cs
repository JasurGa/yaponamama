using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.FavoriteGoods.Queries.GetFavoritesByClientId
{
    public class GetFavoritesByClientIdQueryHandler : IRequestHandler
        <GetFavoritesByClientIdQuery, FavoriteGoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetFavoritesByClientIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<FavoriteGoodListVm> Handle(GetFavoritesByClientIdQuery request,
            CancellationToken cancellationToken)
        {
            var favoriteGoods = await _dbContext.FavoriteGoods
                .Where(x => x.ClientId == request.ClientId)
                .ProjectTo<FavoriteGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new FavoriteGoodListVm { FavoriteGoods = favoriteGoods };
        }
    }
}
