using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator :
        AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PaymentTypeId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodToOrders)
                .NotEmpty();
        }
    }
}
