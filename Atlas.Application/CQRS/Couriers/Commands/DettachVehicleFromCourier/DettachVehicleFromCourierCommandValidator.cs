using Atlas.Application.CQRS.Couriers.Commands.DetachVehicleFromCourier;
using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Couriers.Commands.DettachVehicleFromCourier
{
    public class DettachVehicleFromCourierCommandValidator : AbstractValidator<DettachVehicleFromCourierCommand>
    {
        public DettachVehicleFromCourierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
