using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.DeleteNotification
{
    public class DeleteNotificationCommandHandler
        : IRequestHandler<DeleteNotificationCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeleteNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;
        
        public async Task<Unit> Handle(DeleteNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                throw new NotFoundException(nameof(Notification), request.Id);
            }

            _dbContext.Notifications.Remove(notification);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
