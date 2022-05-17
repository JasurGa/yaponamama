using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class GetStoreListQueryHandler : IRequestHandler<GetStoreListQuery, StoreListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetStoreListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<StoreListVm> Handle(GetStoreListQuery request, CancellationToken cancellationToken)
        {
            var stores = await _dbContext.Stores
                .ProjectTo<StoreLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new StoreListVm { Stores = stores };
        }
    }
}
