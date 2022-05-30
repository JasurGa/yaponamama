using FluentValidation;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumber
{
    public class CreateProviderPhoneNumberCommandValidator : AbstractValidator<CreateProviderPhoneNumberCommand>
    {
        public CreateProviderPhoneNumberCommandValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();
        }
    }
}
