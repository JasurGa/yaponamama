using System;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByProvider
{
    public class GetGoodListByProviderQuery : IRequest<GoodListVm>
    {
        public Guid ProviderId { get; set; }

        public bool ShowDeleted { get; set; }
    }
}
