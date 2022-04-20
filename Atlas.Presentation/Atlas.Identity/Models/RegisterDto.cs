using System;
namespace Atlas.Identity.Models
{
    public class RegisterDto
    {
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
