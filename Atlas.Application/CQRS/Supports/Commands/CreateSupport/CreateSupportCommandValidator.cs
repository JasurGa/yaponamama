using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Supports.Commands.CreateSupport
{
    public class CreateSupportCommandValidator : AbstractValidator<CreateSupportCommand>
    {
        public CreateSupportCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.InternalPhoneNumber)
                .NotEmpty();

            RuleFor(x => x.PassportPhotoPath)
                .NotEmpty();
        }
    }
}
