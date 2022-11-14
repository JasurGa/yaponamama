using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Orders.Commands.ChangeOrderRefundStatus
{
    public class ChangeOrderRefundStatusCommandHandler : IRequestHandler<ChangeOrderRefundStatusCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public ChangeOrderRefundStatusCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(ChangeOrderRefundStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            order.CanRefund = request.CanBeRefund;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

