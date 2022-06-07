using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper.QueryableExtensions;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient
{
    public class GetRecommendationListByClientQueryHandler : IRequestHandler
        <GetRecommendationListByClientQuery, RecommendationListVm>
    {
        private readonly IMapper          _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetRecommendationListByClientQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<RecommendationListVm> Handle(GetRecommendationListByClientQuery request,
            CancellationToken cancellationToken)
        {
            var recommendations = await _dbContext.Recommendations
                .Where(x => x.ClientId == request.ClientId)
                .ProjectTo<RecommendationLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new RecommendationListVm { Recommendations = recommendations };
        }
    }
}
