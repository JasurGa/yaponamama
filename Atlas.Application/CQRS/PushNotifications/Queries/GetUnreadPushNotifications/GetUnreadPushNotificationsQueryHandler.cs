using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetUnreadPushNotifications
{
    public class GetUnreadPushNotificationsQueryHandler : IRequestHandler<GetUnreadPushNotificationsQuery,
        PushNotificationListVm>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetUnreadPushNotificationsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PushNotificationListVm> Handle(GetUnreadPushNotificationsQuery request, CancellationToken cancellationToken)
        {
            var accessibleNotifications = await _dbContext.PushNotifications
                .Where(x => x.ExpiresAt <= DateTime.UtcNow)
                .ProjectTo<PushNotificationLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var readNotificationIds = await _dbContext.PushNotificationReads.Where(x => x.ClientId == request.ClientId)
                .Select(x => x.NotificationId)
                .ToListAsync(cancellationToken);

            var unreadNotifications = accessibleNotifications.Where(x => !readNotificationIds.Contains(x.Id))
                .ToList();

            foreach (var notification in unreadNotifications)
            {
                await _dbContext.PushNotificationReads.AddAsync(new PushNotificationRead
                {
                    Id             = Guid.NewGuid(),
                    ClientId       = request.ClientId,
                    NotificationId = notification.Id,
                    SendAt         = DateTime.UtcNow,
                },
                cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new PushNotificationListVm
            {
                PushNotifications = unreadNotifications.ToList()
            };
        }
    }
}

