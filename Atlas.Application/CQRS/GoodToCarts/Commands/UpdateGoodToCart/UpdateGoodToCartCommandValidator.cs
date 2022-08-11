using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateGoodToCart
{
    public class UpdateGoodToCartCommandValidator : AbstractValidator<UpdateGoodToCartCommand>
    {
        public UpdateGoodToCartCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Count)
                .GreaterThan(0);
        }
    }
}
