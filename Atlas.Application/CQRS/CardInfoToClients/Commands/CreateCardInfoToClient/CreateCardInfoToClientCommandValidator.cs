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

            RuleFor(e => e.CardNumber)
                .NotEmpty();

            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.DateOfIssue)
                .NotEmpty();
        }
    }
}
