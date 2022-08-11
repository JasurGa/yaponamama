using System.Collections.Generic;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GoodToCartListVm
    {
        public IList<GoodToCartLookupDto> GoodToCarts { get; set; }
    }
}
