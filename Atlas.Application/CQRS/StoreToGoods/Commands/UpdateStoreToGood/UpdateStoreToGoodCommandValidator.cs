using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood
{
    public class UpdateStoreToGoodCommandValidator : AbstractValidator<UpdateStoreToGoodCommand>
    {
        public UpdateStoreToGoodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
