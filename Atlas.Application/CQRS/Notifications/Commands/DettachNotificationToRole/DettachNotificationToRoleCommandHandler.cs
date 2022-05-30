using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToRole
{
    public class DettachNotificationToRoleCommandHandler
        : IRequestHandler<DettachNotificationToRoleCommand>
    {
        private readonly IAtlasDbContext _dbContext;

        public DettachNotificationToRoleCommandHandler(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DettachNotificationToRoleCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == request.NotificationId);

            if (notification == null)
            {
                throw new NotFoundException(nameof(Notification), request.NotificationId);
            }

            var userIds = new List<Guid>();
            switch (request.Role)
            {
                case Roles.Admin:
                    userIds = await _dbContext.Admins.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                case Roles.Client:
                    userIds = await _dbContext.Clients.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                case Roles.Courier:
                    userIds = await _dbContext.Couriers.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                case Roles.HeadRecruiter:
                    userIds = await _dbContext.HeadRecruiters.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                case Roles.SupplyManager:
                    userIds = await _dbContext.SupplyManagers.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                case Roles.Support:
                    userIds = await _dbContext.Supports.Select(x => x.UserId)
                        .ToListAsync(cancellationToken);
                    break;
                default:
                    throw new NotFoundException(nameof(Roles), request.Role);
            }

            _dbContext.NotificationAccesses.RemoveRange(await _dbContext
                .NotificationAccesses.Where(x => x.NotificationId == request.NotificationId &&
                    userIds.Contains(x.UserId)).ToListAsync(cancellationToken));

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
