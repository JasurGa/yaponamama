using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.DeleteCardInfoToClient
{
    public class DeleteCardInfoToClientCommandValidator : AbstractValidator<DeleteCardInfoToClientCommand>
    {
        public DeleteCardInfoToClientCommandValidator()
        {
            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
