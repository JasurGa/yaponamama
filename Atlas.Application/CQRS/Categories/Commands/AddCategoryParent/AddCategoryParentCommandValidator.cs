using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Commands.AddCategoryParent
{
    public class AddCategoryParentCommandValidator : AbstractValidator<AddCategoryParentCommand>
    {
        public AddCategoryParentCommandValidator()
        {
            RuleFor(e => e.CategoryId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.ParentId)
                .NotEqual(Guid.Empty);
        }
    }
}
