using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood
{
    public class CreateCategoryToGoodCommandValidator : AbstractValidator<CreateCategoryToGoodCommand>
    {
        public CreateCategoryToGoodCommandValidator()
        {
            RuleFor(ctg => ctg.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(ctg => ctg.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
