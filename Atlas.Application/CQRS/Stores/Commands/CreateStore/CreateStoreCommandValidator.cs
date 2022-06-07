using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Commands.CreateStore
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidator()
        {
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
