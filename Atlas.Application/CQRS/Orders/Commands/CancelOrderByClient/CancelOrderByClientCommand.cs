using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrderByClient
{
    public class CancelOrderByClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}

