using System;
using FluentValidation;

namespace Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback
{
    public class CreateOrderFeedbackCommandValidator : AbstractValidator<CreateOrderFeedbackCommand>
    {
        public CreateOrderFeedbackCommandValidator()
        {
            RuleFor(of => of.OrderId)
                .NotEqual(Guid.NewGuid());

            RuleFor(of => of.Rating)
                .NotEmpty();

            RuleFor(of => of.Text)
                .NotEmpty();
        }
    }
}
