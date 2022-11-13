using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GoodIdListVm
    {
        public ICollection<Guid> GoodIds { get; set; }
    }
}

