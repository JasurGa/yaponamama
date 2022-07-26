using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin
{
    public class GetLastOrdersPagedListByAdminQueryHandler :
        IRequestHandler<GetLastOrdersPagedListByAdminQuery,
            PageDto<OrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLastOrdersPagedListByAdminQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<OrderLookupDto>> Handle(GetLastOrdersPagedListByAdminQuery request,
            CancellationToken cancellationToken)
        {
            var ordersCount = await _dbContext.Orders.CountAsync(cancellationToken);

            var orders = await _dbContext.Orders
                  .Skip(request.PageIndex * request.PageSize)
                  .Take(request.PageSize)
                  .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);

            return new PageDto<OrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = orders
            };
        }
    }
}
