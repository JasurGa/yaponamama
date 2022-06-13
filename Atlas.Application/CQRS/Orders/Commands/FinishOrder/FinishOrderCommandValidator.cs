using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.FinishOrder
{
    public class FinishOrderCommandValidator :
        AbstractValidator<FinishOrderCommand>
    {
        public FinishOrderCommandValidator()
        {
            RuleFor(x => x.CourierId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);
        }
    }
}
