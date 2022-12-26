using System;
using System.Collections.Generic;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GetGoodsByPromoAdvertisePageQuery : IRequest<GoodListVm>
    {
        public Guid PromoAdvertisePageId { get; set; }
    }
}

