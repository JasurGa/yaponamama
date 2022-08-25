using System.Collections.Generic;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrdersByOrder
{
    public class GoodToOrderListVm
    {
        public IList<GoodToOrderLookupDto> GoodToOrders { get; set; }
    }
}
