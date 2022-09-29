using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class Store
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; }

        public string AddressRu { get; set; }

        public string AddressEn { get; set; }

        public string AddressUz { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<StoreToGood> StoreToGoods { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
