using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class GetStoreToGoodByStoreIdQueryHandler : IRequestHandler<GetStoreToGoodByStoreIdQuery, StoreToGoodVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetStoreToGoodByStoreIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<StoreToGoodVm> Handle(GetStoreToGoodByStoreIdQuery request,
            CancellationToken cancellationToken)
        {
            var storeToGood = await _dbContext.StoreToGoods.FirstOrDefaultAsync(x =>
                x.StoreId == request.StoreId, cancellationToken);

            if (storeToGood == null)
            {
                throw new NotFoundException(nameof(StoreToGood), request.StoreId);
            }

            return _mapper.Map<StoreToGood, StoreToGoodVm>(storeToGood);
        }
    }
}
