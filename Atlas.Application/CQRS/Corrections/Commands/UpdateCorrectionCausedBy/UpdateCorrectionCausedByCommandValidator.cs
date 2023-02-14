using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Corrections.Commands.UpdateCorrectionCausedBy
{
    public class UpdateCorrectionCausedByCommandValidator : AbstractValidator<UpdateCorrectionCausedByCommand>
    {
        public UpdateCorrectionCausedByCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CausedBy)
                .NotEmpty();
        }
    }
}
