using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises
{
    public class GetActualPromoAdvertisesQueryHandler : IRequestHandler<GetActualPromoAdvertisesQuery,
        PromoAdvertisesListVm>
    {
        private readonly IMapper         _mapper; 
        private readonly IAtlasDbContext _dbContext;

        public GetActualPromoAdvertisesQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoAdvertisesListVm> Handle(GetActualPromoAdvertisesQuery request, CancellationToken cancellationToken)
        {
            var actualPromoAdvertises = await _dbContext.PromoAdvertises.Where(pa => pa.ExpiresAt > DateTime.UtcNow)
                .OrderBy(x => x.CreatedAt)
                .OrderBy(x => x.OrderNumber)
                .ToListAsync(cancellationToken);

            return new PromoAdvertisesListVm
            {
                PromoAdvertises = _mapper.Map<IEnumerable<PromoAdvertise>,
                    IEnumerable<PromoAdvertiseLookupDto>>(actualPromoAdvertises)
            };
        }
    }
}

