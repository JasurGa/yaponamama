using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
    {
        public DeleteVehicleCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
