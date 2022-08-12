using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class StoreToGood
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public long Count { get; set; }

        public Good Good { get; set; }

        public List<Consignment> Consignments { get; set; }

        public Store Store { get; set; }
    }
}
