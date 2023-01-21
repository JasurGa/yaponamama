using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetPopularGoodList
{
    public class GetPopularGoodListQueryHandler : IRequestHandler<GetPopularGoodListQuery, PopularGoodListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPopularGoodListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PopularGoodListVm> Handle(GetPopularGoodListQuery request, CancellationToken cancellationToken)
        {
            var goods = await _dbContext.Goods
                .OrderByDescending(x => x.GoodToOrders.Count())
                .Take(10)
                .ProjectTo<PopularGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PopularGoodListVm { Goods = goods };
        }
    }
}
