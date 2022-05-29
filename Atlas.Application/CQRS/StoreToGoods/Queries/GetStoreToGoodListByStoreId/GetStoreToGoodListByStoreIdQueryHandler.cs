using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodListByStoreId;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodListByStoreIdQueryHandler : IRequestHandler<GetStoreToGoodListByStoreIdQuery, StoreToGoodListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetStoreToGoodListByStoreIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<StoreToGoodListVm> Handle(GetStoreToGoodListByStoreIdQuery request,
            CancellationToken cancellationToken)
        {
            var storeToGoods = await _dbContext.StoreToGoods
                .Where(x => x.StoreId == request.StoreId)
                .ProjectTo<StoreToGoodLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new StoreToGoodListVm { StoreToGoods = storeToGoods };
        }
    }
}
