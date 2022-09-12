using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.HeadRecruiters.Quieries.FindHeadRecruitersPagedList
{
    public class HeadRecruiterLookupDto : IMapWith<HeadRecruiter>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string PassportPhotoPath { get; set; }

        public DateTime Birthday { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HeadRecruiter, HeadRecruiterLookupDto>()
                .ForMember(x => x.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserId,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.FirstName,
                    opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(x => x.LastName,
                    opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(x => x.AvatarPhotoPath,
                    opt => opt.MapFrom(src => src.User.AvatarPhotoPath))
                .ForMember(x => x.PassportPhotoPath,
                    opt => opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.Birthday,
                    opt => opt.MapFrom(src => src.User.Birthday));
        }
    }
}

