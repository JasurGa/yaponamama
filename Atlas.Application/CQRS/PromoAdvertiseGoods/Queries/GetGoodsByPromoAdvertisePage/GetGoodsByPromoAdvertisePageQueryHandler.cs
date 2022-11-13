using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GetGoodsByPromoAdvertisePageQueryHandler : IRequestHandler<GetGoodsByPromoAdvertisePageQuery,
        GoodIdListVm>
    {
        private readonly IAtlasDbContext _dbContext;

        public GetGoodsByPromoAdvertisePageQueryHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<GoodIdListVm> Handle(GetGoodsByPromoAdvertisePageQuery request, CancellationToken cancellationToken)
        {
            var goodIds = await _dbContext.PromoAdvertiseGoods.Where(x =>
                x.PromoAdvertisePageId == request.PromoAdvertisePageId)
                .Select(x => x.GoodId)
                .ToListAsync(cancellationToken);

            return new GoodIdListVm
            {
                GoodIds = goodIds
            };
        }
    }
}

