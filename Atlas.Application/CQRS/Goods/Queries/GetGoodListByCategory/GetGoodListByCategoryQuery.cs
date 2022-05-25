using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQuery : IRequest<GoodListVm>
    {
        public Guid CategoryId { get; set; }
    }
}
