using System;
namespace Atlas.Domain
{
    public class Consignment
    {
        public Guid Id { get; set; }

        public Guid StoreToGoodId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public int Count { get; set; }

        public string ShelfLocation { get; set; }

        public long CurrentPurchasePrice { get; set; }

        public StoreToGood StoreToGood { get; set; }
    }
}
