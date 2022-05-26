using System;
using FluentValidation;

namespace Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback
{
    public class CreateOrderFeedbackCommandValidator : AbstractValidator<CreateOrderFeedbackCommand>
    {
        public CreateOrderFeedbackCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Rating)
                .NotEmpty();

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}
