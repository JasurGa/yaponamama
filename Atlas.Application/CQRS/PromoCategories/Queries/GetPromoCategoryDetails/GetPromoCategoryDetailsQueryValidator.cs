using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryDetails
{
    public class GetPromoCategoryDetailsQueryValidator : AbstractValidator<GetPromoCategoryDetailsQuery>
    {
        public GetPromoCategoryDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

