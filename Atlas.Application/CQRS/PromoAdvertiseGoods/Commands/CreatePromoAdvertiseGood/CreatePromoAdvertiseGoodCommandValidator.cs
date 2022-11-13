using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.CreatePromoAdvertiseGood
{
    public class CreatePromoAdvertiseGoodCommandValidator : AbstractValidator<CreatePromoAdvertiseGoodCommand>
    {
        public CreatePromoAdvertiseGoodCommandValidator()
        {
            RuleFor(e => e.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PromoAdvertisePageId)
                .NotEqual(Guid.Empty);
        }
    }
}

