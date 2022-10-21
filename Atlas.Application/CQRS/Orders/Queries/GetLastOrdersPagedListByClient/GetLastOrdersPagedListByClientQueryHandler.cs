using System;
using System.Collections.Generic;
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
            var query = _dbContext.Orders.Where(x => x.ClientId == request.ClientId).AsQueryable();

            if (request.ShowActive != false)
            {
                query = query.Where(x => x.FinishedAt != null && x.Status < 3);
            }

            var ordersCount = await query.CountAsync(cancellationToken);
            var orders = await query
                //.Include(x => x.Store)
                .OrderByDescending(x => x.CreatedAt)
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
