using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManagersStoreId
{
    public class UpdateSupplyManagersStoreIdCommandValidator :
        AbstractValidator<UpdateSupplyManagersStoreIdCommand>
    {
        public UpdateSupplyManagersStoreIdCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.SupplyManagersId)
                .NotEmpty();
        }
    }
}
