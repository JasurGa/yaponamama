using System;
namespace Atlas.Domain
{
    public class SupportCall
    {
        public Guid Id { get; set; }

        public Guid SupportId { get; set; }

        public Guid? ClientId { get; set; }

        public string FromPhoneNumber { get; set; }

        public int Duration { get; set; }

        public string CallRecordPath { get; set; }
    }
}
