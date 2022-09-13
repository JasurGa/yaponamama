using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetTopGoods
{
    public class TopGoodListVm
    {
        public List<TopGoodDetailsVm> Categories { get; set; }
    }
}
