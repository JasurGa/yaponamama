using FluentValidation;
using System;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.DeclineVerificationRequest
{
    public class DeclineVerificationRequestCommandValidator : AbstractValidator<DeclineVerificationRequestCommand>
    {
        public DeclineVerificationRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
