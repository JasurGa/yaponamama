using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetAllPagedPromoAdvertises
{
    public class GetAllPagedPromoAdvertisesQueryValidator : AbstractValidator<GetAllPagedPromoAdvertisesQuery>
    {
        public GetAllPagedPromoAdvertisesQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

