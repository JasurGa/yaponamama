using Atlas.Application.CQRS.Orders.Queries.GetOrderPagedList;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin
{
    public class GetOrderPagedListQuery : IRequest<PageDto<OrderLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
