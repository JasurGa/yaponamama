using System;
using MediatR;

namespace Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient
{
    public class CreateAddressToClientCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string PhoneNumber { get; set; }

        public int AddressType { get; set; }

        public bool IsPrivateHouse { get; set; }
    }
}
