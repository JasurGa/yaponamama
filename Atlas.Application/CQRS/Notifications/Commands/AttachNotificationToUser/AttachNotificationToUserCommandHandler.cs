using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToUser
{
    public class AttachNotificationToUserCommandHandler :
        IRequestHandler<AttachNotificationToUserCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public AttachNotificationToUserCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(AttachNotificationToUserCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == request.NotificationId, cancellationToken);

            if (notification == null)
            {
                throw new NotFoundException(nameof(Notification), request.NotificationId);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            await _dbContext.NotificationAccesses.AddAsync(new NotificationAccess
            {
                Id             = Guid.NewGuid(),
                UserId         = request.UserId,
                NotificationId = request.NotificationId,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
