using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.FindOrderPagedList
{
    public class FindOrderPagedListQueryHandler : IRequestHandler<FindOrderPagedListQuery,
        PageDto<OrderLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindOrderPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<OrderLookupDto>> Handle(FindOrderPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var orders = _dbContext.Orders.OrderBy(x => EF.Functions.TrigramsWordSimilarityDistance($"{x.Id}".ToLower().Trim(),
                request.SearchQuery));

            var ordersCount = await orders.CountAsync(cancellationToken);
            var pagedOrders = await orders.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<OrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = _mapper.Map<List<Order>, List<OrderLookupDto>>(pagedOrders),
            };
        }
    }
}

