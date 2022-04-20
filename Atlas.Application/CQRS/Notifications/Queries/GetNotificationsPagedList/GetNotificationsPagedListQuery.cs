using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class GetNotificationsPagedListQuery : IRequest<PageDto<NotificationLookupDto>>
    {
        public Guid UserId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}
