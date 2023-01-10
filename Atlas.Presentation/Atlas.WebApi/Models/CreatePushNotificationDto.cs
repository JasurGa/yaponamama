using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PushNotifications.Commands.CreatePushNotification;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePushNotificationDto : IMapWith<CreatePushNotificationCommand>
    {
        public string HeaderRu { get; set; }

        public string HeaderEn { get; set; }

        public string HeaderUz { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePushNotificationDto, CreatePushNotificationCommand>()
                .ForMember(dst => dst.HeaderRu, opt =>
                    opt.MapFrom(src => src.HeaderRu))
                .ForMember(dst => dst.HeaderEn, opt =>
                    opt.MapFrom(src => src.HeaderEn))
                .ForMember(dst => dst.HeaderUz, opt =>
                    opt.MapFrom(src => src.HeaderUz))
                .ForMember(dst => dst.BodyRu, opt =>
                    opt.MapFrom(src => src.BodyRu))
                .ForMember(dst => dst.BodyEn, opt =>
                    opt.MapFrom(src => src.BodyEn))
                .ForMember(dst => dst.BodyUz, opt =>
                    opt.MapFrom(src => src.BodyUz))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}

