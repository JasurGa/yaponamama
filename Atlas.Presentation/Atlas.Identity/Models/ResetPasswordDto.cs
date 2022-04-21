using System;
namespace Atlas.Identity.Models
{
    public class ResetPasswordDto
    {
        public string VerificationCode { get; set; }

        public string PhoneNumber { get; set; }

        public string NewPassword { get; set; }
    }
}
