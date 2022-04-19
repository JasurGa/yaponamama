using System;
using FluentValidation;

namespace Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient
{
    public class CreateAddressToClientCommandValidator : AbstractValidator<CreateAddressToClientCommand>
    {
        public CreateAddressToClientCommandValidator()
        {
            RuleFor(e => e.Address)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.Latitude)
                .NotEmpty();

            RuleFor(e => e.Longitude)
                .NotEmpty();
        }   
    }
}
