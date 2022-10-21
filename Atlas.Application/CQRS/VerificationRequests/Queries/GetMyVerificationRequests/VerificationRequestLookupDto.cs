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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }

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
                .ForMember(dst => dst.FirstName, opt =>
                    opt.MapFrom(src => src.Client.User.FirstName))
                .ForMember(dst => dst.LastName, opt =>
                    opt.MapFrom(src => src.Client.User.LastName))
                .ForMember(dst => dst.MiddleName, opt =>
                    opt.MapFrom(src => src.Client.User.MiddleName))
                .ForMember(dst => dst.Birthday, opt =>
                    opt.MapFrom(src => src.Client.User.Birthday))
                .ForMember(dst => dst.PhoneNumber, opt =>
                    opt.MapFrom(src => src.Client.PhoneNumber))
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

