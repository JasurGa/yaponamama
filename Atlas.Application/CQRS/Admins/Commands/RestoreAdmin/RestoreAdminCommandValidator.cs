using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Commands.RestoreAdmin
{
    public class RestoreAdminCommandValidator : AbstractValidator<RestoreAdminCommand>
    {
        public RestoreAdminCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
