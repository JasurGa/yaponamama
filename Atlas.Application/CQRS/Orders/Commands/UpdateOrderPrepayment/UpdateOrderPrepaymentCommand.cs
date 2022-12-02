using MediatR;
using System;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPrepayment
{
    public class UpdateOrderPrepaymentCommand : IRequest
    {
        public Guid Id { get; set; }

        public bool IsPrepaid { get; set; }
    }
}
