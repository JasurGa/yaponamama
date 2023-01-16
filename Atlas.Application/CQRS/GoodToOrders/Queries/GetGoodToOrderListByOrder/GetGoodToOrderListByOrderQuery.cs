using System;
using MediatR;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder
{
    public class GetGoodToOrderListByOrderQuery : IRequest<GoodToOrderListVm>
    {
        public Guid OrderId { get; set; }
    }
}