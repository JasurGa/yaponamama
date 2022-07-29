using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Vehicles.Commands.CreateVehicle;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Models
{
    public class CreateVehicleDto : IMapWith<CreateVehicleCommand>
    {
        public string Name { get; set; }

        public string RegistrationCertificatePhotoPath { get; set; }

        public string RegistrationCertificateNumber { get; set; }

        public string RegistrationNumber { get; set; }

        public Guid VehicleTypeId { get; set; }

        public Guid StoreId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateVehicleDto, CreateVehicleCommand>()
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.RegistrationCertificatePhotoPath, opt =>
                    opt.MapFrom(p => p.RegistrationCertificatePhotoPath))
                .ForMember(p => p.RegistrationNumber, opt =>
                    opt.MapFrom(p => p.RegistrationNumber))
                .ForMember(p => p.RegistrationCertificateNumber, opt =>
                    opt.MapFrom(p => p.RegistrationCertificateNumber))
                .ForMember(p => p.VehicleTypeId, opt =>
                    opt.MapFrom(p => p.VehicleTypeId))
                .ForMember(p => p.StoreId, opt =>
                    opt.MapFrom(p => p.StoreId));
        }
    }
}
