using MediatR;
using System;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType
{
    public class UpdateOrderPaymentTypeCommand : IRequest
    {
        public Guid Id { get; set; }

        public int PaymentType { get; set; }
    }
}
