using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.Orders.Queries.GetOrderDetails;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetailsForCourier
{
    public class GetOrderDetailsForCourierQueryHandler : IRequestHandler<GetOrderDetailsForCourierQuery, OrderDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetOrderDetailsForCourierQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<OrderDetailsVm> Handle(GetOrderDetailsForCourierQuery request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.Id, cancellationToken);

            if (order == null || order.CourierId != request.CourierId)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            return _mapper.Map<Order, OrderDetailsVm>(order);
        }
    }
}
