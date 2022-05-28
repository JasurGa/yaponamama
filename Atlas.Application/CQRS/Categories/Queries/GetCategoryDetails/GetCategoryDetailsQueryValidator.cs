using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryValidator : AbstractValidator<GetCategoryDetailsQuery>
    {
        public GetCategoryDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
