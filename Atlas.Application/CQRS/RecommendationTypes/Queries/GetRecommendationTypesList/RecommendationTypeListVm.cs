using System;
using System.Collections;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.RecommendationTypes.Queries.GetRecommendationTypesList
{
    public class RecommendationTypeListVm
    {
        public IList<RecommendationTypeLookupDto> RecommendationTypes { get; set; }
    }
}
