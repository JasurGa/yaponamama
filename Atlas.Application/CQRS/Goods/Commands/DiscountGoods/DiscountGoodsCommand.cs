using MediatR;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Commands.DiscountGoods
{
    public class DiscountGoodsCommand : IRequest
    {
        public IList<Guid> GoodIds { get; set; }

        public float Discount { get; set; }
    }
}
