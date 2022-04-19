using System;
using FluentValidation;

namespace Atlas.Application.CQRS.AddressToClients.Commands.DeleteAddressToClient
{
    public class DeleteAddressToClientCommandValidator : AbstractValidator<DeleteAddressToClientCommand>
    {
        public DeleteAddressToClientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty);

            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
