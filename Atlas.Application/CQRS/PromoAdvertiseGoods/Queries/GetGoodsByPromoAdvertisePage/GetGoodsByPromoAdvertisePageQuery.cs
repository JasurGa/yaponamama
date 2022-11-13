using System;
using System.Collections.Generic;
using MediatR;

namespace Atlas.Application.CQRS.PromoAdvertiseGoods.Queries.GetGoodsByPromoAdvertisePage
{
    public class GetGoodsByPromoAdvertisePageQuery : IRequest<GoodIdListVm>
    {
        public Guid PromoAdvertisePageId { get; set; }
    }
}

