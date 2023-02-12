using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise;
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

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetPromoAdvertiseById
{
    public class GetPromoAdvertiseByIdQueryHandler : IRequestHandler<GetPromoAdvertiseByIdQuery, PromoAdvertiseDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoAdvertiseByIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoAdvertiseDetailsVm> Handle(GetPromoAdvertiseByIdQuery request, CancellationToken cancellationToken)
        {
            var goods = await _dbContext.PromoAdvertiseGoods.Where(x =>
                x.PromoAdvertisePage.PromoAdvertiseId == request.Id)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var promoAdvertisePages = await _dbContext.PromoAdvertisePages
                .Where(x => x.PromoAdvertiseId == request.Id)
                .OrderBy(x => x.OrderNumber)
                .Include(x => x.PromoAdvertiseGoods)
                .ProjectTo<PromoAdvertisePageLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoAdvertiseDetailsVm
            {
                Goods = new GoodListVm
                {
                    Goods = goods
                },
                PromoAdvertisePages = new PromoAdvertisePageListVm 
                { 
                    PromoAdvertisePages = promoAdvertisePages 
                },
            };
        }
    }
}
