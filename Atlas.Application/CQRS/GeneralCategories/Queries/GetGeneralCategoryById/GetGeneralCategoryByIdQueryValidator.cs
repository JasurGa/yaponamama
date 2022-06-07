using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategoryById
{
    public class GetGeneralCategoryByIdQueryValidator :
        AbstractValidator<GetGeneralCategoryByIdQuery>
    {
        public GetGeneralCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
