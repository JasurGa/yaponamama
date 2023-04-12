using FluentValidation;
using System;

namespace Atlas.Application.CQRS.VerificationRequests.Commands.AcceptVerificationRequest
{
    public class AcceptVerificationRequestCommandValidator : AbstractValidator<AcceptVerificationRequestCommand>
    {
        public AcceptVerificationRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
