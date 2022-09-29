using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class GetLastOrdersPagedListByClientQueryHandler : IRequestHandler<GetLastOrdersPagedListByClientQuery, PageDto<ClientOrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetLastOrdersPagedListByClientQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<ClientOrderLookupDto>> Handle(GetLastOrdersPagedListByClientQuery request, CancellationToken cancellationToken)
        {
            var ordersCount = await _dbContext.Orders.CountAsync(x => 
                x.ClientId == request.ClientId,
                cancellationToken);

            var orders = await _dbContext.Orders
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Store)
                .Where(x => x.ClientId == request.ClientId)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ClientOrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<ClientOrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = orders
            };
        }
    }
}
