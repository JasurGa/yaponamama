using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.RecreateGoodToOrders
{
    public class RecreateGoodToOrdersCommandValidator : AbstractValidator<RecreateGoodToOrdersCommand>
    {
        public RecreateGoodToOrdersCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);
        }
    }
}
