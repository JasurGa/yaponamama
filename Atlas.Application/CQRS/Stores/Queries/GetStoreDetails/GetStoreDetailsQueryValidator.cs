using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreDetails
{
    public class GetStoreDetailsQueryValidator : AbstractValidator<GetStoreDetailsQuery>
    {
        public GetStoreDetailsQueryValidator()
        {
            RuleFor(s => s.Id)
                .NotEqual(Guid.NewGuid());
        }
    }
}
