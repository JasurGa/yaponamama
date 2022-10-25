using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Commands.UpdateAddressToClient
{
    public class UpdateAddressToClientCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
