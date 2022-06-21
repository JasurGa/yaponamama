using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryHandler : IRequestHandler<GetProviderListQuery, ProviderListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetProviderListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ProviderListVm> Handle(GetProviderListQuery request, CancellationToken cancellationToken)
        {
            var providers = await _dbContext.Providers
                .Where(x => x.Name.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()) || x.Address.Trim().ToUpper().Contains(request.Search.Trim().ToUpper()))
                .ProjectTo<ProviderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ProviderListVm { Providers = providers };
        }
    }
}
