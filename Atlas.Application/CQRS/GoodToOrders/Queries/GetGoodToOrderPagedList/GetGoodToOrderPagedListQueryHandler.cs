using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderPagedList
{
    public class GetGoodToOrderPagedListQueryHandler : IRequestHandler<GetGoodToOrderPagedListQuery, PageDto<GoodToOrderLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodToOrderPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<GoodToOrderLookupDto>> Handle(GetGoodToOrderPagedListQuery request, CancellationToken cancellationToken)
        {
            var goodToOrdersCount = await _dbContext.GoodToOrders.CountAsync(x => 
                x.Order.Status != (int)OrderStatus.CanceledByAdmin, 
                    cancellationToken);

            var goodToOrders = await _dbContext.GoodToOrders
                .Where(x => x.Order.Status != (int)OrderStatus.CanceledByAdmin)
                .OrderByDescending(x => x.Order.CreatedAt)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize)
                .ProjectTo<GoodToOrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<GoodToOrderLookupDto>
            {
                PageIndex = request.PageIndex,
                TotalCount = goodToOrdersCount,
                PageCount = (int)Math.Ceiling((double)goodToOrdersCount /
                    request.PageSize),
                Data = goodToOrders
            };
        }
    }
}
