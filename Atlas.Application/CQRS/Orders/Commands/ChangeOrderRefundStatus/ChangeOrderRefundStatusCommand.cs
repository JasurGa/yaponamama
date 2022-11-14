using System;
using MediatR;

namespace Atlas.Application.CQRS.Orders.Commands.ChangeOrderRefundStatus
{
    public class ChangeOrderRefundStatusCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public bool CanBeRefund { get; set; }
    }
}

