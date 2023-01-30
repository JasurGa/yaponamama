using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class GetLastOrdersPagedListByCourierQuery : IRequest<PageDto<CourierOrderLookupDto>>
    {
        public Guid CourierId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
