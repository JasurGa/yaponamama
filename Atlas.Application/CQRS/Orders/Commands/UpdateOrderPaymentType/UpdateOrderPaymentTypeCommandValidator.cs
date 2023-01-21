using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType
{
    public class UpdateOrderPaymentTypeCommandValidator : AbstractValidator<UpdateOrderPaymentTypeCommand>
    {
        public UpdateOrderPaymentTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PaymentType)
                .GreaterThanOrEqualTo(0);
        }
    }
}
