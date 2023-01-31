using FluentValidation;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.CreateOrderComment
{
    public class CreateOrderCommentCommandValidator : AbstractValidator<CreateOrderCommentCommand>
    {
        public CreateOrderCommentCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}
