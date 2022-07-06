using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandHandlerValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandHandlerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.OfficialRoleId)
                .NotEqual(Guid.Empty);
        }
    }
}
