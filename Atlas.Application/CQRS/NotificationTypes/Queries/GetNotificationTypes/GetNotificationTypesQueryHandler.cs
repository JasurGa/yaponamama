using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.NotificationTypes.Queries.GetNotificationTypes
{
    public class GetNotificationTypesQueryHandler : IRequestHandler<GetNotificationTypesQuery,
        NotificationTypeListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetNotificationTypesQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<NotificationTypeListVm> Handle(GetNotificationTypesQuery request,
            CancellationToken cancellationToken)
        {
            var notificationTypes = await _dbContext.NotificationTypes
                .ProjectTo<NotificationTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new NotificationTypeListVm
            {
                Notifications = notificationTypes
            };
        }
    }
}
