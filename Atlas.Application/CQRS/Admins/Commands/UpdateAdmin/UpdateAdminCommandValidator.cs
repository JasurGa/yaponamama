using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Commands.UpdateAdmin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        public UpdateAdminCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.OfficialRoleId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();
        }
    }
}
