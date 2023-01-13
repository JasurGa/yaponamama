using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationById
{
    public class GetPushNotificationByIdQueryHandler : IRequestHandler<GetPushNotificationByIdQuery,
        PushNotificationLookupDto>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPushNotificationByIdQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PushNotificationLookupDto> Handle(GetPushNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var pushNotification = await _dbContext.PushNotifications.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (pushNotification == null)
            {
                throw new NotFoundException(nameof(PushNotification), request.Id);
            }

            return _mapper.Map<PushNotification, PushNotificationLookupDto>(pushNotification);
        }
    }
}

