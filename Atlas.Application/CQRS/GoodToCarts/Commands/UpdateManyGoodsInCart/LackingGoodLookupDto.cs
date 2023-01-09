using System;

namespace Atlas.Application.CQRS.GoodToCarts.Commands.UpdateManyGoodsInCart
{
    public class LackingGoodLookupDto
    {
        public Guid GoodId { get; set; }

        public Guid StoreId { get; set; }

        public long MaxCount { get; set; }
    }
}