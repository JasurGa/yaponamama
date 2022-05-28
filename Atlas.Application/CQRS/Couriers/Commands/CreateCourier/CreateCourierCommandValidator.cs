using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Commands.CreateCourier
{
    public class CreateCourierCommandValidator : AbstractValidator<CreateCourierCommand>
    {
        public CreateCourierCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            RuleFor(x => x.PassportPhotoPath)
                .NotEmpty();

            RuleFor(x => x.DriverLicensePath)
                .NotEmpty();
        }
    }
}
