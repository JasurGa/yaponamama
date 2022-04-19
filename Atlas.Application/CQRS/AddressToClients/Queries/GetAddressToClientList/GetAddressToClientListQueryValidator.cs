using System;
using FluentValidation;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList
{
    public class GetAddressToClientListQueryValidator : AbstractValidator<GetAddressToClientListQuery>
    {
        public GetAddressToClientListQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
