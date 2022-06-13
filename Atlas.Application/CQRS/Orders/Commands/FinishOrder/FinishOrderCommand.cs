using System;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.FinishOrder
{
    public class FinishOrderCommand : IRequest
    {
        public Guid CourierId { get; set; }

        public Guid OrderId { get; set; }
    }
}
