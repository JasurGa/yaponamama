using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.UpdateNotification
{
    public class UpdateNotificationCommandHandler
        : IRequestHandler<UpdateNotificationCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdateNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (notification == null)
            {
                throw new NotFoundException(nameof(Notification), request.Id);
            }

            var notificationType = await _dbContext.NotificationTypes.FirstOrDefaultAsync(x =>
                x.Id == request.NotificationTypeId, cancellationToken);

            if (notificationType == null)
            {
                throw new NotFoundException(nameof(NotificationType), request.NotificationTypeId);
            }

            notification.Subject            = request.Subject;
            notification.SubjectRu          = request.SubjectRu;
            notification.SubjectEn          = request.SubjectEn;
            notification.SubjectUz          = request.SubjectUz;
            notification.Body               = request.Body;
            notification.BodyRu             = request.BodyRu;
            notification.BodyEn             = request.BodyEn;
            notification.BodyUz             = request.BodyUz;
            notification.Priority           = request.Priority;
            notification.NotificationTypeId = request.NotificationTypeId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
