using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback
{
    public class CreateOrderFeedbackCommandHandler : IRequestHandler<CreateOrderFeedbackCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateOrderFeedbackCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateOrderFeedbackCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == request.OrderId, cancellationToken);

            if (order == null || order.ClientId != request.ClientId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            var orderFeedback = new OrderFeedback
            {
                Id          = Guid.NewGuid(),
                OrderId     = request.OrderId,
                Rating      = request.Rating,
                Text        = request.Text,
                CreatedAt   = DateTime.UtcNow,
            };

            await _dbContext.OrderFeedbacks.AddAsync(orderFeedback,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderFeedback.Id;
        }
    }
}
