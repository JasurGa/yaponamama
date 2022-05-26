using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory
{
    public class GetGoodListByCategoryQuery : IRequest<GoodListVm>
    {
        public bool ShowDeleted { get; set; }

        public Guid CategoryId { get; set; }
    }
}
