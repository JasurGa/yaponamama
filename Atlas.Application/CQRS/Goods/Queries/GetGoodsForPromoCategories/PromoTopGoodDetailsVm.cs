using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForPromoCategories
{
    public class PromoTopGoodDetailsVm
    {
        public PromoCategoryLookupDto PromoCategory { get; set; }

        public IList<GoodLookupDto> Goods { get; set; }
    }
}