using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Commands.CreateProvider
{
    public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
    {
        public CreateProviderCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Longitude)
                .NotEmpty();

            RuleFor(x => x.Latitude)
                .NotEmpty();
        }
    }
}
