using System;
namespace Atlas.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid? CourierId { get; set; }

        public Guid StoreId { get; set; }

        public Guid ClientId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public int Status { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public Guid PaymentTypeId { get; set; }

        public bool IsPickup { get; set; }

        public Guid? PromoId { get; set; }
    }
}
