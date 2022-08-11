using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{
    public class ChatUserLookupDto : IMapWith<User>
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, ChatUserLookupDto>()
                .ForMember(dst => dst.UserId, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt =>
                    opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.AvatarPhotoPath, opt =>
                    opt.MapFrom(src => src.AvatarPhotoPath));
        }
    }
}
