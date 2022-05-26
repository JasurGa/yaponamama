using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
