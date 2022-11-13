using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GetGoodsByPromoAdvertisePageQueryValidator : AbstractValidator<GetGoodsByPromoAdvertisePageQuery>
    {
        public GetGoodsByPromoAdvertisePageQueryValidator()
        {
            RuleFor(e => e.PromoAdvertisePageId)
                .NotEqual(Guid.Empty);
        }
    }
}

