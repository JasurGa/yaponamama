using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand,
        Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreateNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationType =  await _dbContext.NotificationTypes.FirstOrDefaultAsync(x =>
                x.Id == request.NotificationTypeId, cancellationToken);

            if (notificationType == null)
            {
                throw new NotFoundException(nameof(NotificationType), request.NotificationTypeId);
            }

            var entity = new Notification
            {
                Id                 = Guid.NewGuid(),
                Subject            = request.Subject,
                SubjectRu          = request.SubjectRu,
                SubjectEn          = request.SubjectEn,
                SubjectUz          = request.SubjectUz,
                Body               = request.Body,
                BodyRu             = request.BodyRu,
                BodyEn             = request.BodyEn,
                BodyUz             = request.BodyUz,
                Priority           = request.Priority,
                NotificationTypeId = request.NotificationTypeId
            };

            await _dbContext.Notifications.AddAsync(entity,
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
