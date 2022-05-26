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
