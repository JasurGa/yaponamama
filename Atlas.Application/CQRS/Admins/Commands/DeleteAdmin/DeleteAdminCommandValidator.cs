using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Commands.DeleteAdmin
{
    public class DeleteAdminCommandValidator : AbstractValidator<DeleteAdminCommand>
    {
        public DeleteAdminCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
