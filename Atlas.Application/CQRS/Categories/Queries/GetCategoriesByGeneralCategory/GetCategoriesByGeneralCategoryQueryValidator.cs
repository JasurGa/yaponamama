using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoriesByGeneralCategory
{
    public class GetCategoriesByGeneralCategoryQueryValidator
        : AbstractValidator<GetCategoriesByGeneralCategoryQuery>
    {
        public GetCategoriesByGeneralCategoryQueryValidator()
        {
            RuleFor(x => x.GeneralCategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
