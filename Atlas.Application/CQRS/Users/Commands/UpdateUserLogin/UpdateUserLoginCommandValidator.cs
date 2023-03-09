using Atlas.Application.Common.Extensions;
using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandValidator : AbstractValidator<UpdateUserLoginCommand>
    {
        public UpdateUserLoginCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Login)
                .NotEmpty()
                .PhoneNumber();
        }
    }
}
