using System;
namespace Atlas.Domain
{
    public class VerifyCode
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsVerified { get; set; }

        public string VerificationCode { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
