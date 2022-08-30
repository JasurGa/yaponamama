using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.UpdateGoodToOrder
{
    public class UpdateGoodToOrderCommand : IRequest
    {
        public Guid Id { get; set; }

        public int Count { get; set; }
    }
}
