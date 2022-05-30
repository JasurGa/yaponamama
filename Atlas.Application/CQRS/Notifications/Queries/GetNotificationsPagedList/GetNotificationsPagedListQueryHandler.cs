using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class GetNotificationsPagedListQueryHandler : IRequestHandler<GetNotificationsPagedListQuery,
        PageDto<NotificationLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetNotificationsPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<NotificationLookupDto>> Handle(GetNotificationsPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var notificationsCount = await _dbContext.NotificationAccesses.CountAsync(x =>
                x.UserId == request.UserId, cancellationToken);

            var notifications = await _dbContext.NotificationAccesses
                .Include(x => x.Notification)
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.Notification)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<NotificationLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<NotificationLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = notificationsCount,
                PageCount  = (int)Math.Ceiling((double)notificationsCount / request.PageSize),
                Data       = notifications
            };
        }
    }
}
