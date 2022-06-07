using System;
using MediatR;

namespace Atlas.Application.CQRS.RecommendationTypes.Queries.GetRecommendationTypesList
{
    public class GetRecommendationTypesListQuery : IRequest<RecommendationTypeListVm>
    {
    }
}
