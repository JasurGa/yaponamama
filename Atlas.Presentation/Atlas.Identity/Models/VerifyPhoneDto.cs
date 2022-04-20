using System;
namespace Atlas.Identity.Models
{
    public class VerifyPhoneDto
    {
        public string VerificationCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
