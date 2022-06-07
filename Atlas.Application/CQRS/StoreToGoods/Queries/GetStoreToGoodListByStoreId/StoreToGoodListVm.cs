using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodListByStoreId
{
    public class StoreToGoodListVm
    {
        public IList<StoreToGoodLookupDto> StoreToGoods { get; set; }
    }
}
