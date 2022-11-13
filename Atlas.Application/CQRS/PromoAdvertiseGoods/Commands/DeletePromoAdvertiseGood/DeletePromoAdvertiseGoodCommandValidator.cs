using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.DeletePromoAdvertiseGood
{
    public class DeletePromoAdvertiseGoodCommandValidator : AbstractValidator<DeletePromoAdvertiseGoodCommand>
    {
        public DeletePromoAdvertiseGoodCommandValidator()
        {
            RuleFor(e => e.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PromoAdvertisePageId)
                .NotEqual(Guid.Empty);
        }
    }
}

