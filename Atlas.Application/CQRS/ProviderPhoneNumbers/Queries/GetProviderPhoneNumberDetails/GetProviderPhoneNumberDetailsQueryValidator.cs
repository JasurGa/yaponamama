using FluentValidation;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberDetails
{
    public class GetProviderPhoneNumberDetailsQueryValidator : AbstractValidator<GetProviderPhoneNumberDetailsQuery>
    {
        public GetProviderPhoneNumberDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
