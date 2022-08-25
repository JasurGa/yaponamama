using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrdersByOrder;
using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder
{
    public class GetGoodToOrderListByOrderQuery : IRequest<GoodToOrderListVm>
    {
        public Guid OrderId { get; set; }
    }
}