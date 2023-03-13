using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Goods.Queries.GetGoodDetails;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise
{
    public class GetPagesByPromoAdvertiseQueryHandler : IRequestHandler<GetPagesByPromoAdvertiseQuery,
        PromoAdvertisePageListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPagesByPromoAdvertiseQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoAdvertisePageListVm> Handle(GetPagesByPromoAdvertiseQuery request,
            CancellationToken cancellationToken)
        {
            var promoAdvertisePages = await _dbContext.PromoAdvertisePages
                .Where(x => x.PromoAdvertiseId == request.PromoAdvertiseId)
                .OrderBy(x => x.OrderNumber)
                .Include(x => x.PromoAdvertiseGoods)
                .ProjectTo<PromoAdvertisePageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var page in promoAdvertisePages)
            {
                foreach (var good in page.Goods)
                {
                    good.StoreToCount = await _dbContext.StoreToGoods
                        .Where(x => x.GoodId == good.Id)
                        .Select(x => new StoreToCountLookupDto
                        {
                            StoreId = x.StoreId,
                            Count   = x.Count
                        })
                        .ToListAsync(cancellationToken);
                }
            }

            return new PromoAdvertisePageListVm
            {
                PromoAdvertisePages = promoAdvertisePages
            };
        }
    }
}

