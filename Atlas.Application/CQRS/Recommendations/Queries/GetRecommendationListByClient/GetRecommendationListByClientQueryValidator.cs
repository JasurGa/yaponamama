using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient
{
    public class GetRecommendationListByClientQueryValidator :
        AbstractValidator<GetRecommendationListByClientQuery>
    {
        public GetRecommendationListByClientQueryValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty);
        }
    }
}
