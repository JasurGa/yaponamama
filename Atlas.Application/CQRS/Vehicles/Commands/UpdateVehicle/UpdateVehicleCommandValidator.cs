using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.RegistrationCertificatePhotoPath)
                .NotEmpty();

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty();

            RuleFor(x => x.VehicleTypeId)
                .NotEmpty();

            RuleFor(x => x.StoreId)
                .NotEmpty();
        }
    }
}
