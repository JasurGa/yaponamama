using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder
{
    public class GetGoodToOrderListByOrderQueryHandler : IRequestHandler<GetGoodToOrderListByOrderQuery, GoodToOrderListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodToOrderListByOrderQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext); 

        public async Task<GoodToOrderListVm> Handle(GetGoodToOrderListByOrderQuery request, CancellationToken cancellationToken)
        {
            var goodToOrders = await _dbContext.GoodToOrders
                .Where(x => x.OrderId == request.OrderId)
                .ProjectTo<GoodToOrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GoodToOrderListVm { GoodToOrders = goodToOrders };
        }
    }
}
