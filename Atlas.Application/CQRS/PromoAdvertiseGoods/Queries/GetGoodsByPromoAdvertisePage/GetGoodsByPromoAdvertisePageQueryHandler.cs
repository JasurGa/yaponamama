using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GetGoodsByPromoAdvertisePageQueryHandler : IRequestHandler<GetGoodsByPromoAdvertisePageQuery,
        GoodListVm>
    {
        private readonly IMapper _mapper;

        private readonly IAtlasDbContext _dbContext;

        public GetGoodsByPromoAdvertisePageQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodListVm> Handle(GetGoodsByPromoAdvertisePageQuery request, CancellationToken cancellationToken)
        {
            var goods = await _dbContext.PromoAdvertiseGoods.Where(x =>
                x.PromoAdvertisePageId == request.PromoAdvertisePageId)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm
            {
                Goods = goods
            };
        }
    }
}

