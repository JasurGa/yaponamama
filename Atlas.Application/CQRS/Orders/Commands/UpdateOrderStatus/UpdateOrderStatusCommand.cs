using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public Guid Id { get; set; }

        public int Status { get; set; }
    }
}

