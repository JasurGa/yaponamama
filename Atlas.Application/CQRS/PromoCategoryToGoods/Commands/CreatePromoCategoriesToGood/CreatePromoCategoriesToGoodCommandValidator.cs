using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.CreatePromoCategoriesToGood
{
    public class CreatePromoCategoriesToGoodCommandValidator :
        AbstractValidator<CreatePromoCategoriesToGoodCommand>
    {
        public CreatePromoCategoriesToGoodCommandValidator()
        {
        }
    }
}

