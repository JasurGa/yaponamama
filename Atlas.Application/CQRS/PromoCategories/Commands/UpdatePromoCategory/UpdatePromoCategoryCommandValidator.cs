using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategories.Commands.UpdatePromoCategory
{
    public class UpdatePromoCategoryCommandValidator : AbstractValidator<UpdatePromoCategoryCommand>
    {
        public UpdatePromoCategoryCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

