using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Supports.Commands.CreateSupport;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateSupportDto : IMapWith<CreateSupportCommand>
    {
        public CreateUserDto User { get; set; }

        public string InternalPhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSupportDto, CreateSupportCommand>()
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.InternalPhoneNumber, opt =>
                    opt.MapFrom(x => x.InternalPhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath));
        }
    }
}
