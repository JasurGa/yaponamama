using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderPagedList
{
    public class GetGoodToOrderPagedListQuery : IRequest<PageDto<GoodToOrderLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
