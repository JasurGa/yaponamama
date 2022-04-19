using System;
namespace Atlas.Domain
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RegistrationCertificatePhotoPath { get; set; }

        public string RegistrationNumber { get; set; }

        public Guid VehicleTypeId { get; set; }

        public Guid StoreId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
