using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierByVehicleId
{
    public class GetCourierByVehicleIdQueryValidator :
        AbstractValidator<GetCourierByVehicleIdQuery>
    {
        public GetCourierByVehicleIdQueryValidator()
        {
            RuleFor(x => x.VehicleId)
                .NotEqual(Guid.Empty);
        }
    }
}
