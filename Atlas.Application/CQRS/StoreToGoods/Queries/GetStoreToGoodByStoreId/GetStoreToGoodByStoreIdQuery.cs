using MediatR;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodByStoreIdQuery : IRequest<StoreToGoodVm>
    {
        public Guid StoreId { get; set; }
    }
}
