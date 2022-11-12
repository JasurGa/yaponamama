using System;

namespace Atlas.Domain
{
    public class PromoAdvertiseGood
    {
        public Guid Id { get; set; }

        public Guid PromoAdvertisePageId { get; set; }

        public PromoAdvertisePage PromoAdvertisePage { get; set; }

        public Guid GoodId { get; set; }
    }
}