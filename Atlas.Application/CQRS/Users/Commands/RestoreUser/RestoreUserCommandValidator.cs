using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.RestoreUser
{
    public class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
    {
        public RestoreUserCommandValidator()
        {
            RuleFor(u => u.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
