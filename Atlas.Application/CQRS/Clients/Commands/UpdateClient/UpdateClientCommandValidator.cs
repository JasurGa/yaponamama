using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
