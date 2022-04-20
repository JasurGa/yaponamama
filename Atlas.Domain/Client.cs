using System;
namespace Atlas.Domain
{
    public class Client
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public string PassportPhotoPath { get; set; }

        public bool IsPassportVerified { get; set; }

        public long Balance { get; set; }

        public bool IsDeleted { get; set; }
    }
}
