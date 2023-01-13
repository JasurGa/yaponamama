using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PushNotifications.Commands.UpdatePushNotification
{
    public class UpdatePushNotificationCommandHandler : IRequestHandler<UpdatePushNotificationCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public UpdatePushNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdatePushNotificationCommand request, CancellationToken cancellationToken)
        {
            var pushNotification = await _dbContext.PushNotifications.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (pushNotification == null)
            {
                throw new NotFoundException(nameof(PushNotification), request.Id);
            }

            pushNotification.HeaderRu  = request.HeaderRu;
            pushNotification.HeaderEn  = request.HeaderEn;
            pushNotification.HeaderUz  = request.HeaderUz;
            pushNotification.BodyRu    = request.BodyRu;
            pushNotification.BodyEn    = request.BodyEn;
            pushNotification.BodyUz    = request.BodyUz;
            pushNotification.ExpiresAt = request.ExpiresAt;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

