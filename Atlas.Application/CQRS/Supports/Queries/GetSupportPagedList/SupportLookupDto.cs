using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList
{
    public class SupportLookupDto : IMapWith<Support>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarPhotoPath { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Support, SupportLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, opt =>
                    opt.MapFrom(x => x.UserId))
                .ForMember(x => x.FirstName, opt =>
                    opt.MapFrom(x => x.User.FirstName))
                .ForMember(x => x.LastName, opt =>
                    opt.MapFrom(x => x.User.LastName))
                .ForMember(x => x.AvatarPhotoPath, opt =>
                    opt.MapFrom(x => x.User.AvatarPhotoPath))
                .ForMember(x => x.InternalPhoneNumber, opt =>
                    opt.MapFrom(x => x.InternalPhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath));
        }
    }
}