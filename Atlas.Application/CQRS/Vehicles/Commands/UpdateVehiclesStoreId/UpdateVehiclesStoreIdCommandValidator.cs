using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehiclesStoreId
{
    public class UpdateVehiclesStoreIdCommandValidator :
        AbstractValidator<UpdateVehiclesStoreIdCommand>
    {
        public UpdateVehiclesStoreIdCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.VehicleIds)
                .NotEmpty();
        }
    }
}
