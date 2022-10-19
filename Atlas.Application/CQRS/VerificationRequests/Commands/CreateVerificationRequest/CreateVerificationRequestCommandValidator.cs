using System;
using FluentValidation;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.CreateVerificationRequest
{
    public class CreateVerificationRequestCommandValidator : AbstractValidator<CreateVerificationRequestCommand>
    {
        public CreateVerificationRequestCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PassportPhotoPath)
                .NotNull();

            RuleFor(x => x.SelfieWithPassportPhotoPath)
                .NotNull();
        }
    }
}

