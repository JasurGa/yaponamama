using System;
namespace Atlas.Domain
{
    public class Good
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public StoreToGood StoreToGood { get; set; }

        public float Volume { get; set; }

        public float Mass { get; set; }

        public float Discount { get; set; }

        public bool IsDeleted { get; set; }
    }
}
