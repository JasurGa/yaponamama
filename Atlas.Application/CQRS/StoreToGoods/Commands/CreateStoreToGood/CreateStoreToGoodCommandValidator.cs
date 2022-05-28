using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood
{
    public class CreateStoreToGoodCommandValidator : AbstractValidator<CreateStoreToGoodCommand>
    {
        public CreateStoreToGoodCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);
        }
    }
}
