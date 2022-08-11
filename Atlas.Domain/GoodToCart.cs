using System;

namespace Atlas.Domain
{
    public class GoodToCart
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid ClientId { get; set; }
        
        public int Count { get; set; } 
    }
}
