using System;
namespace Atlas.Domain
{
    public class Store
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}
