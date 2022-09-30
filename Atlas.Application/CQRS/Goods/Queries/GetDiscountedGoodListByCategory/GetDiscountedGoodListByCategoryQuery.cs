using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using MediatR;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetDiscountedGoodListByCategory
{
    public class GetDiscountedGoodListByCategoryQuery : IRequest<GoodListVm>
    {
        public Guid CategoryId { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
