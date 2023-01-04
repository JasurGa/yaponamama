﻿using System;

namespace Atlas.Domain
{
    public class Promo
    {
        public Guid Id { get; set; }

        public Guid? ClientId { get; set; }

        public Guid GoodId { get; set; }

        public Good Good { get; set; }

        public string Name { get; set; }

        public DateTime ExpiresAt { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }
    }
}
