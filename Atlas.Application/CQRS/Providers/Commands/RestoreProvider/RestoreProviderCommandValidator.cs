using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Commands.RestoreProvider
{
    public class RestoreProviderCommandValidator :
        AbstractValidator<RestoreProviderCommand>
    {
        public RestoreProviderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
