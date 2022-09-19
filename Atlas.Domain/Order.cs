using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid? CourierId { get; set; }

        public Guid StoreId { get; set; }

        public Guid ClientId { get; set; }

        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int Apartment { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeliverAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public float PurchasePrice { get; set; }

        public float SellingPrice { get; set; }

        public int Status { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public int PaymentType { get; set; }

        public bool IsPickup { get; set; }

        public Guid? PromoId { get; set; }

        public Client Client { get; set; }

        public Courier Courier { get; set; }

        public Store Store { get; set; }

        public Promo Promo { get; set; }

        public IEnumerable<GoodToOrder> GoodToOrders { get; set; } 
    }
}
