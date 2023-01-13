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

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList
{
    public class GetPushNotificationsPagedListQueryHandler : IRequestHandler<GetPushNotificationsPagedListQuery,
        PageDto<PushNotificationLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPushNotificationsPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<PushNotificationLookupDto>> Handle(GetPushNotificationsPagedListQuery request,
            CancellationToken cancellationToken)
        {
            var pushNotificationsCount = await _dbContext.PushNotifications.CountAsync(cancellationToken);

            var pushNotifications = await _dbContext.PushNotifications
                .Take(request.PageSize)
                .Skip(request.PageIndex * request.PageSize)
                .ProjectTo<PushNotificationLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PageDto<PushNotificationLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = pushNotificationsCount,
                PageCount  = (int)Math.Ceiling((double)pushNotificationsCount / request.PageSize),
                Data       = pushNotifications
            };
        }
    }
}

