using FluentValidation;
using System;

namespace Atlas.Application.CQRS.SupplyManagers.Queries.GetSupplyManagerDetails
{
    public class GetSupplyManagerDetailsQueryValidator : AbstractValidator<GetSupplyManagerDetailsQuery>
    {
        public GetSupplyManagerDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
