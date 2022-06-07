using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.CreateGeneralCategory
{
    public class CreateGeneralCategoryCommandValidator
        : AbstractValidator<CreateGeneralCategoryCommand>
    {
        public CreateGeneralCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
