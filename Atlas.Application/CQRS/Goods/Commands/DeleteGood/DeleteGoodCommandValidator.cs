using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Commands.DeleteGood
{
    public class DeleteGoodCommandValidator : AbstractValidator<DeleteGoodCommand>
    {
        public DeleteGoodCommandValidator()
        {
            RuleFor(g => g.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
