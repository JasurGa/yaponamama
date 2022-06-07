using System.Collections.Generic;

namespace Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient
{
    public class RecommendationListVm
    {
        public IList<RecommendationLookupDto> Recommendations { get; set; }
    }
}