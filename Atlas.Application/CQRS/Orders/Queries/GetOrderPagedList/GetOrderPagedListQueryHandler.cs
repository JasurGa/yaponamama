using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Orders.Queries.GetOrderPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin
{
    public class GetOrderPagedListQueryHandler : IRequestHandler<GetOrderPagedListQuery, PageDto<OrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<OrderLookupDto>> Handle(GetOrderPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var orders = _dbContext.Orders.AsQueryable();
            if (request.FilterIsPrePayed != null)
            {
                orders = orders.Where(x => x.IsPrePayed == request.FilterIsPrePayed);
            }
            if (request.FilterPaymentType != null)
            {
                orders = orders.Where(x => x.PaymentType == request.FilterPaymentType);
            }
            if (request.FilterStatus != null)
            {
                orders = orders.Where(x => x.Status == request.FilterStatus);
            }
            if (request.FilterFromCreatedAt != null)
            {
                orders = orders.Where(x => x.CreatedAt >= request.FilterFromCreatedAt);
            }
            if (request.FilterToCreatedAt != null)
            {
                orders = orders.Where(x => x.CreatedAt <= request.FilterToCreatedAt);
            }

            var ordersCount = await orders.CountAsync(cancellationToken);
            var pagedOrders = await orders.OrderByDescending(x => x.CreatedAt)
                  .Skip(request.PageIndex * request.PageSize)
                  .Take(request.PageSize)
                  .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);

            return new PageDto<OrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = pagedOrders
            };
        }
    }
}
