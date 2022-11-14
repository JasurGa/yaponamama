using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.ChangeOrderRefundStatus
{
    public class ChangeOrderRefundStatusCommandValidator : AbstractValidator<ChangeOrderRefundStatusCommand>
    {
        public ChangeOrderRefundStatusCommandValidator()
        {
            RuleFor(e => e.OrderId)
                .NotEqual(Guid.Empty);
        }
    }
}

