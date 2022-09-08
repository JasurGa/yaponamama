using System;

namespace Atlas.Domain
{
    public class GoodToOrder
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid OrderId { get; set; }

        public int Count { get; set; }

        public Order Order { get; set; }

        public Good Good { get; set; }
    }
}