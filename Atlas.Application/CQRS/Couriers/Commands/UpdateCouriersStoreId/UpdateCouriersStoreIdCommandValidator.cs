using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Commands.UpdateCouriersStoreId
{
    public class UpdateCouriersStoreIdCommandValidator :
        AbstractValidator<UpdateCouriersStoreIdCommand>
    {
        public UpdateCouriersStoreIdCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.CourierIds)
                .NotEmpty();
        }
    }
}
