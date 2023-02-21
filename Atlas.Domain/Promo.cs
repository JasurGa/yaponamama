using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class Promo
    {
        public Guid Id { get; set; }

        public Guid? ClientId { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresAt { get; set; }

        public int DiscountPrice { get; set; }

        public float DiscountPercent { get; set; }

        public bool ForAllGoods { get; set; }

        public bool FreeDelivery { get; set; }

        public List<PromoToGood> PromoToGoods { get; set; }
    }
}
