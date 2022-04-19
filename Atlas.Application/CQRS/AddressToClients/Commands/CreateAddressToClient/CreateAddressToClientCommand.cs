using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient
{
    public class CreateAddressToClientCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
