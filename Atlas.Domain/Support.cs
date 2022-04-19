using System;
namespace Atlas.Domain
{
    public class Support
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsDeleted { get; set; }
    }
}
