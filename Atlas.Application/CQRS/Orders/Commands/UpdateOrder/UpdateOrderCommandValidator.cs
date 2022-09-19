using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator :
        AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CourierId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PromoId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);
        }
    }
}
