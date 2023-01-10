using System;
namespace Atlas.Domain
{
    public class PushNotification
    {
        public Guid Id { get; set; }

        public string HeaderRu { get; set; }

        public string HeaderEn { get; set; }

        public string HeaderUz { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}

