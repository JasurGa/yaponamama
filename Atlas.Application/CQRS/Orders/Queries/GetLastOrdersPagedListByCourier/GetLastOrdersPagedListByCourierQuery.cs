using System;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class GetLastOrdersPagedListByCourierQuery : IRequest<PageDto<OrderLookupDto>>
    {
        public Guid CourierId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
