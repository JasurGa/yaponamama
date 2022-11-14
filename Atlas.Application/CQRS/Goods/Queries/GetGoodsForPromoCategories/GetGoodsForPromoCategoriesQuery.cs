using System;
using Atlas.Application.CQRS.Goods.Queries.GetTopGoods;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForPromoCategories
{
    public class GetGoodsForPromoCategoriesQuery : IRequest<PromoTopGoodListVm>
    {
        public bool ShowDeleted { get; set; }
    }
}

