using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Users.Queries.GetUserDetails
{
    public class UserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt =>
                    opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Birthday, opt =>
                    opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.AvatarPhotoPath, opt =>
                    opt.MapFrom(src => src.AvatarPhotoPath));
        }
    }
}
