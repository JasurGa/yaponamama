using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList
{
    public class NotificationLookupDto : IMapWith<Notification>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid NotificationTypeId { get; set; }

        public string Subject { get; set; }

        public string Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.NotificationTypeId, opt =>
                    opt.MapFrom(src => src.NotificationTypeId))
                .ForMember(dest => dest.Subject, opt =>
                    opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Priority, opt =>
                    opt.MapFrom(src => src.Priority));
        }
    }
}
