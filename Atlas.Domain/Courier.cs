using System;
namespace Atlas.Domain
{
    public class Courier
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public long Balance { get; set; }

        public long KPI { get; set; }

        public Guid? VehicleId { get; set; }

        public bool IsDeleted { get; set; }

        public Vehicle Vehicle { get; set; }

        public User User { get; set; }
    }
}
