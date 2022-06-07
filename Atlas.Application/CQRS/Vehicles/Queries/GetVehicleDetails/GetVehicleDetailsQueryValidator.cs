using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class GetVehicleDetailsQueryValidator : AbstractValidator<GetVehicleDetailsQuery>
    {
        public GetVehicleDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
