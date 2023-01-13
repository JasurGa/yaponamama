using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList
{
    public class PushNotificationLookupDto : IMapWith<PushNotification>
    {
        public Guid Id { get; set; }

        public string HeaderRu { get; set; }

        public string HeaderEn { get; set; }

        public string HeaderUz { get; set; }

        public string BodyRu { get; set; }

        public string BodyEn { get; set; }

        public string BodyUz { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PushNotification, PushNotificationLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
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
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}