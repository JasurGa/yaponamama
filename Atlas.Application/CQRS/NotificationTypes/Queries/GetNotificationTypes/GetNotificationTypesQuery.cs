using System;
using MediatR;

namespace Atlas.Application.CQRS.NotificationTypes.Queries.GetNotificationTypes
{
    public class GetNotificationTypesQuery : IRequest<NotificationTypeListVm>
    {
    }
}
