using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Commands.DeleteGoodToOrder
{
    public class DeleteGoodToOrderCommandValidator : AbstractValidator<DeleteGoodToOrderCommand>
    {
        public DeleteGoodToOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
