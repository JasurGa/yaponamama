using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.VerificationRequests.Commands.CreateVerificationRequest;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateVerificationRequestDto : IMapWith<CreateVerificationRequestCommand>
    {
        public string PassportPhotoPath { get; set; }

        public string SelfieWithPassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateVerificationRequestDto, CreateVerificationRequestCommand>()
                .ForMember(dst => dst.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(dst => dst.SelfieWithPassportPhotoPath, opt =>
                    opt.MapFrom(src => src.SelfieWithPassportPhotoPath));
        }
    }
}

