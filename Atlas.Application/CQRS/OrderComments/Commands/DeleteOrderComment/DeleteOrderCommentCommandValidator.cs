using FluentValidation;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.DeleteOrderComment
{
    public class DeleteOrderCommentCommandValidator : AbstractValidator<DeleteOrderCommentCommand>
    {
        public DeleteOrderCommentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
