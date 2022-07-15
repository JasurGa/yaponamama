using System;
using FluentValidation;

namespace Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager
{
    public class CreateSupplyManagerCommandValidator : AbstractValidator<CreateSupplyManagerCommand>
    {
        public CreateSupplyManagerCommandValidator()
        {
            RuleFor(x => x.StoreId)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            RuleFor(x => x.PassportPhotoPath)
                .NotEmpty();
        }
    }
}
