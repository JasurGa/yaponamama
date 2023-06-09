﻿using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery,
        ClientOrderDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<ClientOrderDetailsVm> Handle(GetOrderDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(x => x.Client.User)
                .Include(x => x.Courier.User)
                .Include(x => x.Promo)
                .Include(x => x.Store)
                .Include(x => x.GoodToOrders).ThenInclude(x => x.Good.Provider)
                .Include(x => x.GoodToOrders).ThenInclude(x => x.Good.StoreToGoods)
                .FirstOrDefaultAsync(x => x.Id == request.Id, 
                    cancellationToken);

            if (order == null || order.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            return _mapper.Map<Order, ClientOrderDetailsVm>(order);
        }
    }
}
