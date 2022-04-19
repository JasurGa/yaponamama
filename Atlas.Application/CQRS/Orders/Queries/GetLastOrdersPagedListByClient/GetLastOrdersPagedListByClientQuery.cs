using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class GetLastOrdersPagedListByClientQuery : IRequest<PageDto<OrderLookupDto>>
    {
        public Guid ClientId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
}
