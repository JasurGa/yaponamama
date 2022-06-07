using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.RestoreGeneralCategory
{
    public class RestoreGeneralCategoryCommandValidator :
        AbstractValidator<RestoreGeneralCategoryCommand>
    {
        public RestoreGeneralCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
