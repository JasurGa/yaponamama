using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Commands.DeleteAddressToClient
{
    public class DeleteAddressToClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
    }
}
