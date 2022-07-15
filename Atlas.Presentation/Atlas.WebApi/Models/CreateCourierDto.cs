using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Couriers.Commands.CreateCourier;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateCourierDto : IMapWith<CreateCourierCommand>
    {
        public CreateUserDto User { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public string DriverLicensePath { get; set; }

        public Guid VehicleId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourierDto, CreateCourierCommand>()
                .ForMember(x => x.User, opt =>
                    opt.MapFrom(x => x.User))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath))
                .ForMember(x => x.DriverLicensePath, opt =>
                    opt.MapFrom(x => x.DriverLicensePath))
                .ForMember(x => x.VehicleId, opt =>
                    opt.MapFrom(x => x.VehicleId));
        }
    }
}
