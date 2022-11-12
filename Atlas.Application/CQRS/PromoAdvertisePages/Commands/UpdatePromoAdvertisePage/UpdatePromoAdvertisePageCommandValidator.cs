using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.UpdatePromoAdvertisePage
{
    public class UpdatePromoAdvertisePageCommandValidator : AbstractValidator<UpdatePromoAdvertisePageCommand>
    {
        public UpdatePromoAdvertisePageCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PromoAdvertiseId)
                .NotEqual(Guid.Empty);
        }
    }
}

