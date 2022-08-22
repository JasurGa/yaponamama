using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class Provider
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string LogotypePath { get; set; }

        public ICollection<Good> Goods { get; set; }

        public ICollection<ProviderPhoneNumber> ProviderPhoneNumbers { get; set; }

        public bool IsDeleted { get; set; }
    }
}
