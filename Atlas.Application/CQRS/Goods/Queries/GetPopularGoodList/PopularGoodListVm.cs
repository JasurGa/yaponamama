using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetPopularGoodList
{
    public class PopularGoodListVm
    {
        public IList<PopularGoodLookupDto> Goods { get; set; }
    }
}
