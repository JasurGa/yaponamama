using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertises.Commands.UpdatePromoAdvertise
{
    public class UpdatePromoAdvertiseCommandValidator : AbstractValidator<UpdatePromoAdvertiseCommand>
    {
        public UpdatePromoAdvertiseCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}

