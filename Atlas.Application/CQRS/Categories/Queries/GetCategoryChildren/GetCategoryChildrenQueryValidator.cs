using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryChildren
{
    public class GetCategoryChildrenQueryValidator :
        AbstractValidator<GetCategoryChildrenQuery>
    {
        public GetCategoryChildrenQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
