using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.CreateGoodToCart
{
    public class CreateGoodToCartCommandValidator : AbstractValidator<CreateGoodToCartCommand>
    {
        public CreateGoodToCartCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Count)
                .GreaterThan(0);
        }
    }
}
