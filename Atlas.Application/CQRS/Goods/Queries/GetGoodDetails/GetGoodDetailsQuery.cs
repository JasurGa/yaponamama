using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GetGoodDetailsQuery : IRequest<GoodDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
