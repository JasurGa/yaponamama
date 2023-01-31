using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.OrderComments.Commands.CreateOrderComment
{
    public class CreateOrderCommentCommandHandler : IRequestHandler<CreateOrderCommentCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateOrderCommentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateOrderCommentCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => 
                x.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new NotFoundException(nameof(Order), cancellationToken);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), cancellationToken);
            }

            var orderComment = new OrderComment
            {
                Id        = Guid.NewGuid(),
                OrderId   = request.OrderId,
                UserId    = request.UserId,
                Text      = request.Text,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.AddAsync(orderComment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderComment.Id;
        }
    }
}
