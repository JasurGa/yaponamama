using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.OrderComments.Commands.UpdateOrderComment
{
    public class UpdateOrderCommentCommandHandler : IRequestHandler<UpdateOrderCommentCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateOrderCommentCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateOrderCommentCommand request, CancellationToken cancellationToken)
        {
            var orderComment = await _dbContext.OrderComments.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (orderComment == null || orderComment.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(OrderComment), request.Id);
            }

            orderComment.Text = request.Text;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
