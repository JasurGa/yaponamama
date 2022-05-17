using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
