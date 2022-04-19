using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientList
{
    public class GetCardInfoToClientListQueryValidator : AbstractValidator<GetCardInfoToClientListQuery>
    {
        public GetCardInfoToClientListQueryValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
