using System;

namespace Atlas.Domain 
{
    public class VerificationRequest 
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string PassportPhotoPath { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public bool IsVerified { get; set; }

        public bool IsChecked { get; set; }

        public string Comment { get; set; }

        public DateTime SendAt { get; set; }
    }
}