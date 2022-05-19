using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
    {
        public UpdateStoreCommandValidator()
        {
            RuleFor(s => s.Id)
               .NotEqual(Guid.NewGuid());

            RuleFor(s => s.Name)
                .NotEmpty();

            RuleFor(s => s.Address)
                .NotEmpty();

            RuleFor(s => s.Latitude)
                .NotEmpty();

            RuleFor(s => s.Longitude)
                .NotEmpty();
        }
    }
}
