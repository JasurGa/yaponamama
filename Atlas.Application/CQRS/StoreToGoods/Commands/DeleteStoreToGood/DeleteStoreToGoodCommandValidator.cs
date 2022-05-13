using System;
using FluentValidation;

namespace Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood
{
    public class DeleteStoreToGoodCommandValidator : AbstractValidator<DeleteStoreToGoodCommand>
    {
        public DeleteStoreToGoodCommandValidator()
        {
            RuleFor(stg => stg.Id).
                NotEqual(Guid.Empty);
        }
    }
}
