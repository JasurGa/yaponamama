using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails
{
    public class NotificationDetailsVm : IMapWith<Notification>
    {
        public Guid Id { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationDetailsVm>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NotificationTypeId, opt =>
                    opt.MapFrom(src => src.NotificationTypeId))
                .ForMember(dest => dest.Subject, opt =>
                    opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.Priority, opt =>
                    opt.MapFrom(src => src.Priority));
        }
    }
}
