using System;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByStore
{
    public class GetLastOrdersPagedListByStoreQuery : IRequest<PageDto<OrderLookupDto>>
    {
        public Guid StoreId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
