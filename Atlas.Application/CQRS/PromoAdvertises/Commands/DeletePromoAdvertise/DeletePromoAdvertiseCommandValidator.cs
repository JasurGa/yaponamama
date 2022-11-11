using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.DeletePromoAdvertise
{
    public class DeletePromoAdvertiseCommandValidator : AbstractValidator<DeletePromoAdvertiseCommand>
    {
        public DeletePromoAdvertiseCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

