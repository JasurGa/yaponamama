using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class GetPromoDetailsQueryValidator : AbstractValidator<GetPromoDetailsQuery>
    {
        public GetPromoDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
