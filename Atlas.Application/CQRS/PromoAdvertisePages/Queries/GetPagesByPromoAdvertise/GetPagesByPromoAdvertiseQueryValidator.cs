using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Queries.GetPagesByPromoAdvertise
{
    public class GetPagesByPromoAdvertiseQueryValidator : AbstractValidator<GetPagesByPromoAdvertiseQuery>
    {
        public GetPagesByPromoAdvertiseQueryValidator()
        {
            RuleFor(e => e.PromoAdvertiseId)
                .NotEqual(Guid.Empty);
        }
    }
}

