using System;
namespace Atlas.Domain
{
    public class ForgotPasswordCode
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string VerificationCode { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
