using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCourier;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateCourierDto : IMapWith<UpdateCourierCommand>
    {        
        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCourierDto, UpdateCourierCommand>()
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.DriverLicensePath, opt =>
                    opt.MapFrom(src => src.DriverLicensePath));
        }
    }
}
