using System;
using FluentValidation;

namespace Atlas.Application.CQRS.CardInfoToClients.Commands.UpdateCardInfoToClient
{
    public class UpdateCardInfoToClientCommandValidator : AbstractValidator<UpdateCardInfoToClientCommand>
    {
        public UpdateCardInfoToClientCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);

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
