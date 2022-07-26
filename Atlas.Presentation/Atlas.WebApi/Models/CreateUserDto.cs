using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Users.Commands.CreateUser;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? MiddleName { get; set; }

        public int Sex { get; set; }

        public DateTime? Birthday { get; set; }

        public string AvatarPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, CreateUserDto>()
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src =>
                    src.Login))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src =>
                    src.Password))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src =>
                    src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src =>
                    src.LastName))
                .ForMember(dst => dst.Sex, opt => opt.MapFrom(src =>
                    src.Sex))
                .ForMember(dst => dst.Birthday, opt => opt.MapFrom(src =>
                    src.Birthday))
                .ForMember(dst => dst.AvatarPhotoPath, opt => opt.MapFrom(src =>
                    src.AvatarPhotoPath));
        }
    }
}
