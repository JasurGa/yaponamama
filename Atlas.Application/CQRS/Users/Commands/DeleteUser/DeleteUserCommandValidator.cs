using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
