using System;
namespace Atlas.Domain
{
    public class AddressToClient
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int AddressType { get; set; }

        public DateTime CreatedAt { get; set; }

        public Client Client { get; set; }
    }
}
