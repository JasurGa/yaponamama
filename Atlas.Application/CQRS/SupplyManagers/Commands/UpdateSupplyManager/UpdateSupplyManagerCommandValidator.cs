using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManager
{
    public class UpdateSupplyManagerCommandValidator : AbstractValidator<UpdateSupplyManagerCommand>
    {
        public UpdateSupplyManagerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            RuleFor(x => x.PassportPhotoPath)
                .NotEmpty();
        }
    }
}
