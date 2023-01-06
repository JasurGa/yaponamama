using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetLastPromoUsagesPaged
{
    public class GetLastPromoUsagesPagedQueryValidator : AbstractValidator<GetLastPromoUsagesPagedQuery>
    {
        public GetLastPromoUsagesPagedQueryValidator()
        {
            RuleFor(e => e.PageSize)
                .GreaterThan(0);

            RuleFor(e => e.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

