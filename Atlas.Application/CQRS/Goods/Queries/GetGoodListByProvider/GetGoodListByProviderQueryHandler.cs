using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Atlas.Application.Common.Exceptions;
using Atlas.Domain;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodListByProvider
{
    public class GetGoodListByProviderQueryHandler : IRequestHandler<GetGoodListByProviderQuery,
        GoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodListByProviderQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodListVm> Handle(GetGoodListByProviderQuery request,
            CancellationToken cancellationToken)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x =>
                x.Id == request.ProviderId, cancellationToken);

            if (provider == null)
            {
                throw new NotFoundException(nameof(Provider), request.ProviderId);
            }

            var goods = await _dbContext.Goods.OrderBy(x => x.NameRu)
                .Where(x => x.ProviderId == request.ProviderId && x.IsDeleted == request.ShowDeleted)
                .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodListVm { Goods = goods };
        }
    }
}
