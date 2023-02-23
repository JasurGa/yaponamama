using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GetGoodToCartListQueryHandler : IRequestHandler<GetGoodToCartListQuery, GoodToCartListVm>
    {
        private readonly IAtlasDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGoodToCartListQueryHandler(IAtlasDbContext dbContext, IMapper mapper) =>
           (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GoodToCartListVm> Handle(GetGoodToCartListQuery request, CancellationToken cancellationToken)
        {
            var goodToCarts = await _dbContext.GoodToCarts
                .Where(x => x.ClientId == request.ClientId)
                .Include(x => x.Good)
                .ThenInclude(x => x.StoreToGoods)
                .ProjectTo<GoodToCartLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodToCartListVm { GoodToCarts = goodToCarts };
        }
    }
}
