using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertisePages.Commands.CreatePromoAdvertisePage
{
    public class CreatePromoAdvertisePageCommandValidator : AbstractValidator<CreatePromoAdvertisePageCommand>
    {
        public CreatePromoAdvertisePageCommandValidator()
        {
            RuleFor(e => e.PromoAdvertiseId)
                .NotEqual(Guid.Empty);
        }
    }
}

