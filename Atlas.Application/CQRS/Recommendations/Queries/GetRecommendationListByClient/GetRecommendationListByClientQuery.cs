using System;
using MediatR;

namespace Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient
{
    public class GetRecommendationListByClientQuery : IRequest<RecommendationListVm>
    {
        public Guid ClientId { get; set; }
    }
}
