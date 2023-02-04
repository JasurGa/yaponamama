using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Corrections.Commands.CreateCorrection
{
    public class CreateCorrectionCommandValidator : AbstractValidator<CreateCorrectionCommand>
    {
        public CreateCorrectionCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
