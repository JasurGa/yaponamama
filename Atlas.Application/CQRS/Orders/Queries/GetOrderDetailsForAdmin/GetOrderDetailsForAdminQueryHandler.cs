using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForAdmin
{
    public class GetOrderDetailsForAdminQueryHandler : IRequestHandler<
        GetOrderDetailsForAdminQuery, OrderDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderDetailsForAdminQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OrderDetailsVm> Handle(GetOrderDetailsForAdminQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .Include(x => x.Courier)
                    .ThenInclude(y => y.User)
                .Include(x => x.Client)
                    .ThenInclude(y => y.User)
                .Include(x => x.GoodToOrders)
                .Include(x => x.Store)
                .Include(x => x.Promo)
                .FirstOrDefaultAsync(x => x.Id == request.Id,
                    cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            return _mapper.Map<Order, OrderDetailsVm>(order);
        }
    }
}
