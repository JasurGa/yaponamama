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

        public string SubjectRu { get; set; }

        public string SubjectEn { get; set; }

        public string SubjectUz { get; set; }

        public string Body { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public int Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNotificationDto, CreateNotificationCommand>()
                .ForMember(dst => dst.NotificationTypeId, opt =>
                    opt.MapFrom(src => src.NotificationTypeId))
                .ForMember(dst => dst.Subject, opt =>
                    opt.MapFrom(src => src.Subject))
                .ForMember(dst => dst.SubjectRu, opt =>
                    opt.MapFrom(src => src.SubjectRu))
                .ForMember(dst => dst.SubjectEn, opt =>
                    opt.MapFrom(src => src.SubjectEn))
                .ForMember(dst => dst.SubjectUz, opt =>
                    opt.MapFrom(src => src.SubjectUz))
                .ForMember(dst => dst.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.BodyRu, opt =>
                    opt.MapFrom(src => src.BodyRu))
                .ForMember(dst => dst.BodyEn, opt =>
                    opt.MapFrom(src => src.BodyEn))
                .ForMember(dst => dst.BodyUz, opt =>
                    opt.MapFrom(src => src.BodyUz))
                .ForMember(dst => dst.Priority, opt =>
                    opt.MapFrom(src => src.Priority));
        }
    }
}
