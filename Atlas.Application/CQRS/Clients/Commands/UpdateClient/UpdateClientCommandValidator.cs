using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MaximumLength(13);
        }
    }
}
