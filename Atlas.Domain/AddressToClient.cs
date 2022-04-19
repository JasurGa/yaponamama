using System;
namespace Atlas.Domain
{
    public class AddressToClient
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public Guid ClientId { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
