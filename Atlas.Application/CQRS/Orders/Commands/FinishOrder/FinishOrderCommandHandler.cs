using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Enums;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.FinishOrder
{
    public class FinishOrderCommandHandler :
        IRequestHandler<FinishOrderCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public FinishOrderCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(FinishOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.OrderId, cancellationToken);

            if (order == null || order.CourierId != request.CourierId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            order.FinishedAt = DateTime.UtcNow;
            order.Status     = (int)OrderStatus.Finished;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
