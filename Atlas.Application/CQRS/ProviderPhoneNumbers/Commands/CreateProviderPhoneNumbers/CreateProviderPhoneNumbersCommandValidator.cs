using System;
using FluentValidation;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumbers
{
    public class CreateProviderPhoneNumbersCommandValidator :
        AbstractValidator<CreateProviderPhoneNumbersCommand>
    {
        public CreateProviderPhoneNumbersCommandValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumbers)
                .NotEmpty();
        }
    }
}
