using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForPromoCategories
{
    public class PromoTopGoodListVm
    {
        public List<PromoTopGoodDetailsVm> PromoCategories { get; set; }
    }
}