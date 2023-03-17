using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientDetails
{
    public class GetCardInfoToClientDetailsQueryValidator : AbstractValidator<GetCardInfoToClientDetailsQuery>
    {
        public GetCardInfoToClientDetailsQueryValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
