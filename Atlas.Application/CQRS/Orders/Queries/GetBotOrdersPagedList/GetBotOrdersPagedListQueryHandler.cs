﻿using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class GetBotOrdersPagedListQueryHandler : IRequestHandler<GetBotOrdersPagedListQuery, PageDto<BotOrderLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetBotOrdersPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<BotOrderLookupDto>> Handle(GetBotOrdersPagedListQuery request, CancellationToken cancellationToken)
        {
            var ordersCount = 0;
            if (request.Status != null)
            {
                ordersCount = await _dbContext.Orders.CountAsync(x => x.ClientId == request.ClientId &&
                    x.Status == request.Status, cancellationToken);
            }
            else
            {
                ordersCount = await _dbContext.Orders.CountAsync(x => x.ClientId == request.ClientId,
                    cancellationToken);
            }

            var ordersQuery = _dbContext.Orders
                .Where(x => x.ClientId == request.ClientId);

            if (request.Status != null)
            {
                ordersQuery = ordersQuery
                    .Where(x => x.Status == request.Status);
            }
            if (request.GetCanceled)
            {
                ordersQuery = ordersQuery.Where(x => 
                    x.Status == (int)OrderStatus.CanceledByAdmin ||
                    x.Status == (int)OrderStatus.CanceledByUser);
            }

            var orders = await ordersQuery
                .OrderByDescending(x => x.CreatedAt)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<BotOrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<BotOrderLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = ordersCount,
                PageCount  = (int)Math.Ceiling((double)ordersCount / request.PageSize),
                Data       = orders
            };
        }
    }
}
