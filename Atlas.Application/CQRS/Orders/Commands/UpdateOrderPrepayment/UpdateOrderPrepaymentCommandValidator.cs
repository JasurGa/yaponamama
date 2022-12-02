using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPrepayment
{
    public class UpdateOrderPrepaymentCommandValidator : AbstractValidator<UpdateOrderPrepaymentCommand>
    {
        public UpdateOrderPrepaymentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
