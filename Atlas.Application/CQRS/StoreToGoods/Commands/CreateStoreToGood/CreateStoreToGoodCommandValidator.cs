using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood
{
    public class CreateStoreToGoodCommandValidator : AbstractValidator<CreateStoreToGoodCommand>
    {
        public CreateStoreToGoodCommandValidator()
        {
            RuleFor(stg => stg.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(stg => stg.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(stg => stg.Count)
                .NotEmpty();
        }
    }
}
