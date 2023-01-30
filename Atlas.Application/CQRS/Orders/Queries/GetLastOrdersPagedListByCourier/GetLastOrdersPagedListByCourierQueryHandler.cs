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

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class GetLastOrdersPagedListByCourierQueryHandler : IRequestHandler<GetLastOrdersPagedListByCourierQuery, PageDto<CourierOrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLastOrdersPagedListByCourierQueryHandler(IMapper mapper, IAtlasDbContext dbContext) => 
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<CourierOrderLookupDto>> Handle(GetLastOrdersPagedListByCourierQuery request, CancellationToken cancellationToken)
        {
            var ordersCount = await _dbContext.Orders.CountAsync(x => 
                x.CourierId == request.CourierId, 
                    cancellationToken);

            var orders = await _dbContext.Orders
                    .Where(x => x.CourierId == request.CourierId)
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(request.PageIndex * request.PageSize)
                    .Take(request.PageSize)
                    .ProjectTo<CourierOrderLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            return new PageDto<CourierOrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = orders,
            };
        }
    }
}
