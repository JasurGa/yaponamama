using System;
using Atlas.Application.CQRS.Notifications.Commands.UpdateNotification;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateNotificationDto
    {
        public Guid Id { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNotificationDto, UpdateNotificationCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
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
