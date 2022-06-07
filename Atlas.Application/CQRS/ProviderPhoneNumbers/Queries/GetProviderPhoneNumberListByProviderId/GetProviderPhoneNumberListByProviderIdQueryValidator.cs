using FluentValidation;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId
{
    public class GetProviderPhoneNumberListByProviderIdQueryValidator : AbstractValidator<GetProviderPhoneNumberListByProviderIdQuery>
    {
        public GetProviderPhoneNumberListByProviderIdQueryValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotEqual(Guid.Empty);
        }
    }
}
