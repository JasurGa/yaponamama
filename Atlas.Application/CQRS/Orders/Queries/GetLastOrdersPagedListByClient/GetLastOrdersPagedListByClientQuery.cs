using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class GetLastOrdersPagedListByClientQuery : IRequest<PageDto<ClientOrderLookupDto>>
    {
        public Guid ClientId { get; set; }

        public bool ShowActive { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
