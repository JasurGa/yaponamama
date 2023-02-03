using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Goods.Queries.GetDiscountedGoodList
{
    public class GetDiscountedGoodListQueryHandler : IRequestHandler<GetDiscountedGoodListQuery, GoodListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetDiscountedGoodListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodListVm> Handle(GetDiscountedGoodListQuery request, CancellationToken cancellationToken)
        {
            var goods = await _dbContext.Goods
                .Where(x => x.IsDeleted == false & x.Discount != 0)
                .OrderByDescending(x => x.StoreToGoods.Select(x => x.Count).Sum())
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
