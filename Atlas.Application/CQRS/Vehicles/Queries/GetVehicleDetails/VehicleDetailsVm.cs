using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleDetails
{
    public class VehicleDetailsVm : IMapWith<Vehicle>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RegistrationCertificatePhotoPath { get; set; }

        public string RegistrationNumber { get; set; }

        public Guid VehicleTypeId { get; set; }

        public Guid StoreId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.RegistrationCertificatePhotoPath, opt =>
                    opt.MapFrom(src => src.RegistrationCertificatePhotoPath))
                .ForMember(dst => dst.RegistrationNumber, opt =>
                    opt.MapFrom(src => src.RegistrationNumber))
                .ForMember(dst => dst.VehicleTypeId, opt =>
                    opt.MapFrom(src => src.VehicleTypeId))
                .ForMember(dst => dst.StoreId, opt =>
                    opt.MapFrom(src => src.StoreId));
        }
    }
}
