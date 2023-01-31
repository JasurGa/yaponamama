using FluentValidation;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.UpdateOrderComment
{
    public class UpdateOrderCommentCommandValidator : AbstractValidator<UpdateOrderCommentCommand>
    {
        public UpdateOrderCommentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}
