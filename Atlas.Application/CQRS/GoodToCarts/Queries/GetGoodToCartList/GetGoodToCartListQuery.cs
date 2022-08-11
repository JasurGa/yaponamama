using MediatR;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GetGoodToCartListQuery : IRequest<GoodToCartListVm>
    {
        public Guid ClientId { get; set; }
    }
}
