using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.RestoreUser
{
    public class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
    {
        public RestoreUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
