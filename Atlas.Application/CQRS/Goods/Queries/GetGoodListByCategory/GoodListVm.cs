using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GoodListVm
    {
        public IList<GoodLookupDto> Goods { get; set; }
    }
}
