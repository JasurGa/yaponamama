using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryParents
{
    public class GetCategoryParentsQueryValidator : AbstractValidator<GetCategoryParentsQuery>
    {
        public GetCategoryParentsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
