using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommandValidator : AbstractValidator<RestoreCategoryCommand>
    {
        public RestoreCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
