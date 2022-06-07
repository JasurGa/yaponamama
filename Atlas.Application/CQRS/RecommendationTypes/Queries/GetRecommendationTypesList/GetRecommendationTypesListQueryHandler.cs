using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.RecommendationTypes.Queries.GetRecommendationTypesList
{
    public class GetRecommendationTypesListQueryHandler : IRequestHandler
        <GetRecommendationTypesListQuery, RecommendationTypeListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetRecommendationTypesListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<RecommendationTypeListVm> Handle(GetRecommendationTypesListQuery request,
            CancellationToken cancellationToken)
        {
            var recommendationTypes = await _dbContext.RecommendationTypes
                .ProjectTo<RecommendationTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new RecommendationTypeListVm
            {
                RecommendationTypes = recommendationTypes
            };
        }
    }
}
