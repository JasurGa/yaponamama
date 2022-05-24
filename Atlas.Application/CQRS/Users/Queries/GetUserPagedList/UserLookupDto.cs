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
                .ForMember(eVm => eVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eVm => eVm.Login,
                    opt => opt.MapFrom(e => e.Login))
                .ForMember(eVm => eVm.FirstName,
                    opt => opt.MapFrom(e => e.FirstName))
                .ForMember(eVm => eVm.LastName,
                    opt => opt.MapFrom(e => e.LastName))
                .ForMember(eVm => eVm.CreatedAt,
                    opt => opt.MapFrom(e => e.CreatedAt))
                .ForMember(eVm => eVm.Birthday,
                    opt => opt.MapFrom(e => e.Birthday))
                .ForMember(eVm => eVm.AvatarPhotoPath,
                    opt => opt.MapFrom(e => e.AvatarPhotoPath));
        }
    }
}
