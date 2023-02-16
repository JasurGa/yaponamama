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

namespace Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId
{
    public class GetPromoToGoodsByPromoIdQueryHandler : IRequestHandler<GetPromoToGoodsByPromoIdQuery, PromoToGoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoToGoodsByPromoIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoToGoodListVm> Handle(GetPromoToGoodsByPromoIdQuery request, CancellationToken cancellationToken)
        {
            var promoToGoods = await _dbContext.PromoToGoods
                .Where(x => x.PromoId == request.PromoId)
                .Include(x => x.Good)
                .ProjectTo<PromoToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PromoToGoodListVm
            {
                PromoToGoods = promoToGoods
            };
        }
    }
}
