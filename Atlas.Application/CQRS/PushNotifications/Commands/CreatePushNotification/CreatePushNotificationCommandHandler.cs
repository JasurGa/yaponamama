using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Commands.CreatePushNotification
{
    public class CreatePushNotificationCommandHandler : IRequestHandler<CreatePushNotificationCommand, Guid>
    {
        private readonly IAtlasDbContext _dbContext;

        public CreatePushNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreatePushNotificationCommand request, CancellationToken cancellationToken)
        {
            var pushNotification = new PushNotification
            {
                Id        = Guid.NewGuid(),
                HeaderRu  = request.HeaderRu,
                HeaderEn  = request.HeaderEn,
                HeaderUz  = request.HeaderUz,
                BodyRu    = request.BodyRu,
                BodyEn    = request.BodyEn,
                BodyUz    = request.BodyUz,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = request.ExpiresAt,
            };

            await _dbContext.PushNotifications.AddAsync(pushNotification,
                cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return pushNotification.Id;
        }
    }
}

