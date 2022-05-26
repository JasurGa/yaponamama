using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.CreateCategoryToGood
{
    public class CreateCategoryToGoodCommandValidator : AbstractValidator<CreateCategoryToGoodCommand>
    {
        public CreateCategoryToGoodCommandValidator()
        {
            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);
        }
    }
}
