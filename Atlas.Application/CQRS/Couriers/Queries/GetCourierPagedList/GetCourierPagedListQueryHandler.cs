using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList
{
    public class GetCourierPagedListQueryHandler : IRequestHandler<GetCourierPagedListQuery, PageDto<CourierLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCourierPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<CourierLookupDto>> Handle(GetCourierPagedListQuery request, CancellationToken cancellationToken)
        {
            var couriersCount = await _dbContext.Couriers.CountAsync(x => 
                x.IsDeleted == request.ShowDeleted, cancellationToken);

            var couriers = await _dbContext.Couriers
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .Include(x => x.User)
                .ProjectTo<CourierLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<CourierLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = couriersCount,
                PageCount = (int)Math.Ceiling((double)couriersCount / request.PageSize),
                Data = couriers,
            };
        }
    }
}
