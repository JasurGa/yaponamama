using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class NotificationLookupDto : IMapWith<Notification>
    {
        public Guid Id { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string SubjectRu { get; set; }

        public string SubjectEn { get; set; }

        public string SubjectUz { get; set; }

        public string Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
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
                .ForMember(dst => dst.Priority, opt =>
                    opt.MapFrom(src => src.Priority));
        }
    }
}
