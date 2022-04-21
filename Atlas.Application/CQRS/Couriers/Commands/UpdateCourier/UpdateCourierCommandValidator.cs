using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCourier
{
    public class UpdateCourierCommandValidator : AbstractValidator<UpdateCourierCommand>
    {
        public UpdateCourierCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(13);

            RuleFor(e => e.VehicleId)
                .NotEqual(Guid.Empty);
        }
    }
}
