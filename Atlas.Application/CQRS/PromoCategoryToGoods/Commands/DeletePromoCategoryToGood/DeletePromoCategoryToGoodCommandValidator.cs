using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PromoCategoryToGoods.Commands.DeletePromoCategoryToGood
{
    public class DeletePromoCategoryToGoodCommandValidator :
        AbstractValidator<DeletePromoCategoryToGoodCommand>
    {
        public DeletePromoCategoryToGoodCommandValidator()
        {
        }
    }
}

