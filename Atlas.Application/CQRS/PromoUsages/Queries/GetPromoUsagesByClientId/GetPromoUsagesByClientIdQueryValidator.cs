using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId
{
    public class GetPromoUsagesByClientIdQueryValidator : AbstractValidator<GetPromoUsagesByClientIdQuery>
    {
        public GetPromoUsagesByClientIdQueryValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}

