using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetLastUsagesPaged
{
    public class GetLastUsagesPagedQueryValidator : AbstractValidator<GetLastUsagesPagedQuery>
    {
        public GetLastUsagesPagedQueryValidator()
        {
            RuleFor(e => e.PageSize)
                .GreaterThan(0);

            RuleFor(e => e.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

