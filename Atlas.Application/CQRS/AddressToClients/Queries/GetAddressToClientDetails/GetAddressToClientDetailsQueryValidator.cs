using System;
using FluentValidation;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientDetails
{
    public class GetAddressToClientDetailsQueryValidator : AbstractValidator<GetAddressToClientDetailsQuery>
    {
        public GetAddressToClientDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
