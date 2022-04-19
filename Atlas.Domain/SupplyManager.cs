using System;
namespace Atlas.Domain
{
    public class SupplyManager
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public Guid StoreId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
