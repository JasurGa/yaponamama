using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.DeleteGeneralCategory
{
    public class DeleteGeneralCategoryCommandValidator :
        AbstractValidator<DeleteGeneralCategoryCommand>
    {
        public DeleteGeneralCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
