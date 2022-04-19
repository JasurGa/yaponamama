using System;
using FluentValidation;

namespace Atlas.Application.CQRS.AddressToClients.Commands.UpdateAddressToClient
{
    public class UpdateAddressToClientCommandValidator : AbstractValidator<UpdateAddressToClientCommand>
    {
        public UpdateAddressToClientCommandValidator()
        {
            RuleFor(e => e.Address)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(e => e.Latitude)
                .NotEmpty();

            RuleFor(e => e.Longitude)
                .NotEmpty();

            RuleFor(e => e.ClientId)
                .NotEqual(Guid.Empty);

            RuleFor(e => e.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
