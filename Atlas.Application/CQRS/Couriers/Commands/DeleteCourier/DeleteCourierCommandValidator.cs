using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Commands.DeleteCourier
{
    public class DeleteCourierCommandValidator : AbstractValidator<DeleteCourierCommand>
    {
        public DeleteCourierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
