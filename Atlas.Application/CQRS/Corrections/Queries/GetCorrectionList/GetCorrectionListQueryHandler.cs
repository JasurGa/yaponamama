using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList
{
    public class GetCorrectionListQueryHandler : IRequestHandler<GetCorrectionListQuery, CorrectionListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetCorrectionListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<CorrectionListVm> Handle(GetCorrectionListQuery request, CancellationToken cancellationToken)
        {
            var corrections = await _dbContext.Corrections
                .ProjectTo<CorrectionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CorrectionListVm 
            { 
                Corrections = corrections 
            };
        }
    }
}
