using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.OrderComments.Commands.DeleteOrderComment
{
    public class DeleteOrderCommentCommandHandler : IRequestHandler<DeleteOrderCommentCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteOrderCommentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteOrderCommentCommand request, CancellationToken cancellationToken)
        {
            var orderComment = await _dbContext.OrderComments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (orderComment == null || orderComment.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(OrderComment), request.Id);
            }

            _dbContext.OrderComments.Remove(orderComment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
