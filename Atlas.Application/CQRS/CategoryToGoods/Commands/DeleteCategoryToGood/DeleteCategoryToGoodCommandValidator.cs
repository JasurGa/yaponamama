using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommandValidator : AbstractValidator<DeleteCategoryToGoodCommand>
    {
        public DeleteCategoryToGoodCommandValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

        }
    }
}
