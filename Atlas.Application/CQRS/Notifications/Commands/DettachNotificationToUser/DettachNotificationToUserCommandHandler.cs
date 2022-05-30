using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToUser
{
    public class DettachNotificationToUserCommandHandler :
        IRequestHandler<DettachNotificationToUserCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DettachNotificationToUserCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DettachNotificationToUserCommand request,
            CancellationToken cancellationToken)
        {
            var notificationAccess = await _dbContext.NotificationAccesses
                .FirstOrDefaultAsync(x => x.NotificationId == request.NotificationId &&
                    x.UserId == request.UserId, cancellationToken);

            if (notificationAccess == null)
            {
                throw new NotFoundException(nameof(Notification), request.NotificationId);
            }

            _dbContext.NotificationAccesses.Remove(notificationAccess);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
