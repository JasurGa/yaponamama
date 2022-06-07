using FluentValidation;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.DeleteProviderPhoneNumber
{
    public class DeleteProviderPhoneNumberCommandValidator : AbstractValidator<DeleteProviderPhoneNumberCommand>
    {
        public DeleteProviderPhoneNumberCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
