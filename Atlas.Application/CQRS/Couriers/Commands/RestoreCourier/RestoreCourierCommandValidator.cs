using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Couriers.Commands.RestoreCourier
{
    public class RestoreCourierCommandValidator : AbstractValidator<RestoreCourierCommand>
    {
        public RestoreCourierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
