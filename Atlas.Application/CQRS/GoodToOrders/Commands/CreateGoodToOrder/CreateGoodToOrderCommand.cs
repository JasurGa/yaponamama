using System;
using MediatR;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder
{
    public class CreateGoodToOrderCommand : IRequest<Guid>
    {
        public Guid GoodId { get; set; }

        public Guid OrderId { get; set; }

        public Guid StoreId { get; set; }

        public int Count { get; set; }
    }
}
