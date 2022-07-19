using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.MiddleName)
                .NotEmpty();

            RuleFor(x => x.Birthday)
                .NotEmpty();

            RuleFor(x => x.Sex)
                //.InclusiveBetween(0, Enum.GetNames(typeof(UserSex)).Length);
                .InclusiveBetween(0, 1);
        }
    }
}
