using System;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.FindOrderPagedList
{
    public class FindOrderPagedListQuery : IRequest<PageDto<OrderLookupDto>>
    {
        public string SearchQuery { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}

