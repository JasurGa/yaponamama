using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback
{
    public class CreateOrderFeedbackCommandHandler : IRequestHandler<CreateOrderFeedbackCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateOrderFeedbackCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateOrderFeedbackCommand request, CancellationToken cancellationToken)
        {
            var orderFeedback = new OrderFeedback
            {
                OrderId     = request.OrderId,
                Rating      = request.Rating,
                Text        = request.Text,
                CreatedAt   = DateTime.Now,
            };

            await _dbContext.OrderFeedbacks.AddAsync(orderFeedback, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderFeedback.Id;
        }
    }
}
