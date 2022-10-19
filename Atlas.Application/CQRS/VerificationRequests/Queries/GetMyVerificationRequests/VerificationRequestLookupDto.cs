using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests
{
    public class VerificationRequestLookupDto : IMapWith<VerificationRequest>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string PassportPhotoPath { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public bool IsVerified { get; set; }

        public bool IsChecked { get; set; }

        public string Comment { get; set; }

        public DateTime SendAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerificationRequest, VerificationRequestLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(dst => dst.SelfieWithPassportPhotoPath, opt =>
                    opt.MapFrom(src => src.SelfieWithPassportPhotoPath))
                .ForMember(dst => dst.IsVerified, opt =>
                    opt.MapFrom(src => src.IsVerified))
                .ForMember(dst => dst.IsChecked, opt =>
                    opt.MapFrom(src => src.IsChecked))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.SendAt, opt =>
                    opt.MapFrom(src => src.SendAt));
        }
    }
}

