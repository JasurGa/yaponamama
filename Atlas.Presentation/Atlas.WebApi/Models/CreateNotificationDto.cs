using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Notifications.Commands.CreateNotification;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateNotificationDto : IMapWith<CreateNotificationCommand>
    {
        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNotificationDto, CreateNotificationCommand>()
                .ForMember(dst => dst.NotificationTypeId, opt =>
                    opt.MapFrom(src => src.NotificationTypeId))
                .ForMember(dst => dst.Subject, opt =>
                    opt.MapFrom(src => src.Subject))
                .ForMember(dst => dst.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.Priority, opt =>
                    opt.MapFrom(src => src.Priority));
        }
    }
}
