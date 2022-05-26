using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Commands.RestoreGood
{
    public class RestoreGoodCommandValidator : AbstractValidator<RestoreGoodCommand>
    {
        public RestoreGoodCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
