using System;
using FluentValidation;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder
{
    public class CreateGoodToOrderCommandValidator :
        AbstractValidator<CreateGoodToOrderCommand>
    {
        public CreateGoodToOrderCommandValidator()
        {
            RuleFor(x => x.GoodId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
