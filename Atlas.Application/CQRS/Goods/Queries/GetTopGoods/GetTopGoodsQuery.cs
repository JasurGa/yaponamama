using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetTopGoods
{
    public class GetTopGoodsQuery : IRequest<TopGoodListVm>
    {
        public Guid CategoryId { get; set; }
    }
}
