using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PushNotifications.Commands.DeletePushNotification
{
    public class DeletePushNotificationCommandHandler : IRequestHandler<DeletePushNotificationCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DeletePushNotificationCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeletePushNotificationCommand request, CancellationToken cancellationToken)
        {
            var pushNotification = await _dbContext.PushNotifications.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (pushNotification == null)
            {
                throw new NotFoundException(nameof(PushNotification), request.Id);
            }

            _dbContext.PushNotifications.Remove(pushNotification);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

