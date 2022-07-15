using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.UpdateSupport
{
    public class UpdateSupportCommandValidator : AbstractValidator<UpdateSupportCommand>
    {
        public UpdateSupportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.InternalPhoneNumber)
                .NotEmpty();

            RuleFor(x => x.PassportPhotoPath)
                .NotEmpty();
        }
    }
}
