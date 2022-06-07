using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCourier;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateCourierDto : IMapWith<UpdateCourierCommand>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public Guid VehicleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCourierDto, UpdateCourierCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(src => src.PassportPhotoPath))
                .ForMember(x => x.DriverLicensePath, opt =>
                    opt.MapFrom(src => src.DriverLicensePath))
                .ForMember(x => x.VehicleId, opt =>
                    opt.MapFrom(src => src.VehicleId));
        }
    }
}
