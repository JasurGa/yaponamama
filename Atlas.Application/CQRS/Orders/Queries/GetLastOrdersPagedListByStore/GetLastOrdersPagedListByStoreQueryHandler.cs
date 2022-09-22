using System;
using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Models;
using Atlas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByStore
{
    public class GetLastOrdersPagedListByCourierQueryHandler : IRequestHandler<GetLastOrdersPagedListByStoreQuery, PageDto<OrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLastOrdersPagedListByCourierQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<OrderLookupDto>> Handle(GetLastOrdersPagedListByStoreQuery request, CancellationToken cancellationToken)
        {
            var ordersCount = await _dbContext.Orders.CountAsync(x =>
                x.StoreId == request.StoreId, cancellationToken);

            var orders = await _dbContext.Orders
                    .Where(x => x.StoreId == request.StoreId)
                    .OrderByDescending(x => x.CreatedAt)
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
