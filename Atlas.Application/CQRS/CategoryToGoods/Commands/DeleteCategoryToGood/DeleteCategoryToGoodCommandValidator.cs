using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CategoryToGoods.Commands.DeleteCategoryToGood
{
    public class DeleteCategoryToGoodCommandValidator : AbstractValidator<DeleteCategoryToGoodCommand>
    {
        public DeleteCategoryToGoodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
