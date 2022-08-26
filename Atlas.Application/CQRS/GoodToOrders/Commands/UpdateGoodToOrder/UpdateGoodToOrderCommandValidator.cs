using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.UpdateGoodToOrder
{
    public class UpdateGoodToOrderCommandValidator : AbstractValidator<UpdateGoodToOrderCommand>
    {
        public UpdateGoodToOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.Count)
                .NotEmpty();
        }
    }
}