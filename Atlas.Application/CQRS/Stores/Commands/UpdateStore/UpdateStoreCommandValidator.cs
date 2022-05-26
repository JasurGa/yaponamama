using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
    {
        public UpdateStoreCommandValidator()
        {
            RuleFor(x => x.Id)
               .NotEqual(Guid.Empty);

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.Latitude)
                .NotEmpty();

            RuleFor(x => x.Longitude)
                .NotEmpty();
        }
    }
}
