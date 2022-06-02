using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.UpdateGeneralCategory
{
    public class UpdateGeneralCategoryCommandValidator :
        AbstractValidator<UpdateGeneralCategoryCommand>
    {
        public UpdateGeneralCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
