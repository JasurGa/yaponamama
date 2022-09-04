using FluentValidation;
using System;

namespace Atlas.Application.CQRS.Clients.Commands.RestoreClient
{
    public class RestoreClientCommandValidator : AbstractValidator<RestoreClientCommand>
    {
        public RestoreClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
