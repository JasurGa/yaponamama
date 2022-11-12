using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class PromoAdvertise
    {
        public Guid Id { get; set; }

        public string WideBackground { get; set; }

        public string HighBackground { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<PromoAdvertisePage> PromoAdvertisePages { get; set; }
    }
}

