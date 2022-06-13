using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public Guid ClientId { get; set; }
    }
}
