using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList;
using Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetAllPagedPromoAdvertises
{
    public class GetAllPagedPromoAdvertisesQueryHandler : IRequestHandler<GetAllPagedPromoAdvertisesQuery,
        PageDto<PromoAdvertiseLookupDto>>
    {
        private readonly IMapper _mapper;

        private readonly IAtlasDbContext _dbContext;

        public GetAllPagedPromoAdvertisesQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<PromoAdvertiseLookupDto>> Handle(GetAllPagedPromoAdvertisesQuery request,
            CancellationToken cancellationToken)
        {
            var promoAdvertisesCount = await _dbContext.PromoAdvertises
                .CountAsync(cancellationToken);

            var promoAdvertises = await _dbContext.PromoAdvertises
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<PromoAdvertiseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<PromoAdvertiseLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = promoAdvertisesCount,
                PageCount  = (int)Math.Ceiling((double)promoAdvertisesCount / request.PageSize),
                Data       = promoAdvertises,
            };
        }
    }
}

