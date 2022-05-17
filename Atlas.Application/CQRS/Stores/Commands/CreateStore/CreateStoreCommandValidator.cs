using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Commands.CreateStore
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidator()
        {
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
