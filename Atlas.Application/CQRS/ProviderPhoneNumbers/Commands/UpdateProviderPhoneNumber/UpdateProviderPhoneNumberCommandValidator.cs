using FluentValidation;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.UpdateProviderPhoneNumber
{
    public class UpdateProviderPhoneNumberCommandValidator : AbstractValidator<UpdateProviderPhoneNumberCommand>
    {
        public UpdateProviderPhoneNumberCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();
        }
    }
}
