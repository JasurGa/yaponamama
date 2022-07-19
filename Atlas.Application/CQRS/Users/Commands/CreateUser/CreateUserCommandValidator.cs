using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Sex)
                //.InclusiveBetween(0, Enum.GetNames(typeof(UserSex)).Length);
                .InclusiveBetween(0, 1);
        }
    }
}
