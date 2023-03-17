using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient
{
    public class CreateCardInfoToClientCommandValidator : AbstractValidator<CreateCardInfoToClientCommand>
    {
        public CreateCardInfoToClientCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty();

            RuleFor(e => e.Number)
                .NotEmpty();

            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.Expire)
                .NotEmpty();
        }
    }
}
