using System.Collections;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoToGoods.Queries.GetPromoToGoodsByPromoId
{
    public class PromoToGoodListVm
    {
        public List<PromoToGoodLookupDto> PromoToGoods { get; set; }
    }
}