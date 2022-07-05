using System;
using MediatR;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodCounts
{
    public class GetGoodCountsQuery : IRequest<int>
    {
        public Guid CategoryId { get; set; }
    }
}
