using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategories.Commands.RestorePromoCategory
{
    public class RestorePromoCategoryCommandValidator : AbstractValidator<RestorePromoCategoryCommand>
    {
        public RestorePromoCategoryCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

