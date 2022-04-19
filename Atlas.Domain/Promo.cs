using System;

namespace Atlas.Domain
{
    public class Promo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }
    }
}
