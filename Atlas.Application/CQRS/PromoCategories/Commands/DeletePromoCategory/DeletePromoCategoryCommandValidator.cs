using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategories.Commands.DeletePromoCategory
{
    public class DeletePromoCategoryCommandValidator : AbstractValidator<DeletePromoCategoryCommand>
    {
        public DeletePromoCategoryCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

