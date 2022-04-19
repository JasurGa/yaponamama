using System;
namespace Atlas.Domain
{
    public class ProviderPhoneNumber
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
