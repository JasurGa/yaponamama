using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertises.Queries.GetActualPromoAdvertises
{
    public class GetActualPromoAdvertisesQueryValidator : AbstractValidator<GetActualPromoAdvertisesQuery>
    {
        public GetActualPromoAdvertisesQueryValidator()
        {
        }
    }
}

