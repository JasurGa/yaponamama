using System;
using Atlas.Application.CQRS.Goods.Queries.GetTopGoods;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodsForMainCategories
{
    public class GetGoodsForMainCategoriesQuery : IRequest<TopGoodListVm>
    {
    }
}
