using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.DeleteGoodToCart
{
    public class DeleteGoodToCartCommandValidator : AbstractValidator<DeleteGoodToCartCommand>
    {
        public DeleteGoodToCartCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
            
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
