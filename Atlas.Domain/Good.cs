﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Atlas.Domain
{
    public class Good
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string Description { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string NoteRu { get; set; }

        public string NoteUz { get; set; }

        public string NoteEn { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public float Discount { get; set; }

        public float Volume { get; set; }

        public float Mass { get; set; }

        public bool IsDeleted { get; set; }

        public string CodeIkpu { get; set; }

        public int SaleTaxPercent { get; set; }

        public string PackageCode { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsVerified { get; set; }

        public ICollection<StoreToGood> StoreToGoods { get; set; }

        public ICollection<PhotoToGood> PhotoToGoods { get; set; }

        public ICollection<FavoriteGood> FavoriteGoods { get; set; }

        public ICollection<GoodToOrder> GoodToOrders { get; set; }

        public ICollection<PromoToGood> PromoToGoods { get; set; }

        public Provider Provider { get; set; }
    }
}
