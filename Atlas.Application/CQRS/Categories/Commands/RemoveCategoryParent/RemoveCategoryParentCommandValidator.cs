using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Commands.RemoveCategoryParent
{
    public class RemoveCategoryParentCommandValidator : AbstractValidator<RemoveCategoryParentCommand>
    {
        public RemoveCategoryParentCommandValidator()
        {
            RuleFor(e => e.ParentId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
