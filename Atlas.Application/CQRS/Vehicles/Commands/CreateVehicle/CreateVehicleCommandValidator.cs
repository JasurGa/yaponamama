using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
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
