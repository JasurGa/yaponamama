using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Users.Queries.GetUserPagedList
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Birthday { get; set; }

        public string AvatarPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Login, opt =>
                    opt.MapFrom(x => x.Login))
                .ForMember(x => x.FirstName, opt =>
                    opt.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opt =>
                    opt.MapFrom(x => x.LastName))
                .ForMember(x => x.CreatedAt, opt =>
                    opt.MapFrom(x => x.CreatedAt))
                .ForMember(x => x.Birthday, opt =>
                    opt.MapFrom(x => x.Birthday))
                .ForMember(x => x.AvatarPhotoPath, opt =>
                    opt.MapFrom(x => x.AvatarPhotoPath));
        }
    }
}
