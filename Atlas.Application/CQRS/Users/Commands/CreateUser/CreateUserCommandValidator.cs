using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
        }
    }
}
