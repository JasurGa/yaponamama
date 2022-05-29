using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodListByStoreId
{
    public class GetStoreToGoodListByStoreIdQuery : IRequest<StoreToGoodListVm>
    {
        public Guid StoreId { get; set; }
    }
}
