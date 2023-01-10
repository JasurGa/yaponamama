using System;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using AutoMapper;
using MediatR;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList
{
    public class GetPushNotificationsPagedListQuery : IRequest<PageDto<PushNotificationLookupDto>>
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}

