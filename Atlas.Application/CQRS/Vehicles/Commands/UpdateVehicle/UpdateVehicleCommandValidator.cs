using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEqual(Guid.Empty);

            RuleFor(v => v.Name)
                .NotEmpty();

            RuleFor(v => v.RegistrationCertificatePhotoPath)
                .NotEmpty();

            RuleFor(v => v.RegistrationNumber)
                .NotEmpty();

            RuleFor(v => v.VehicleTypeId)
                .NotEmpty();

            RuleFor(v => v.StoreId)
                .NotEmpty();
        }
    }
}
