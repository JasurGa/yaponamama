using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoListByClientId
{
    public class GetPromoListByClientIdQueryValidator : AbstractValidator<GetPromoListByClientIdQuery>
    {
        public GetPromoListByClientIdQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
