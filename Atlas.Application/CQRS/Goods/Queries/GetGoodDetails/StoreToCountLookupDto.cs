using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class StoreToCountLookupDto
    {
        public Guid StoreId { get; set; }

        public long Count { get; set; }
    }
}