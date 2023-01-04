using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetByClientId
{
    public class GetByClientIdQueryValidator : AbstractValidator<GetByClientIdQuery>
    {
        public GetByClientIdQueryValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}

