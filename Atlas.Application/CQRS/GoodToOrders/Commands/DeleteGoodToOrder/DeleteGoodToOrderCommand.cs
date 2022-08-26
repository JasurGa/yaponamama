using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.DeleteGoodToOrder
{
    public class DeleteGoodToOrderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
