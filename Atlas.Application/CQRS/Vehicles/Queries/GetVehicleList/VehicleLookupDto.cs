using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Vehicles.Queries.GetVehicleList
{
    public class VehicleLookupDto : IMapWith<Vehicle>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RegistrationNumber { get; set; }
        
        public string RegistrationCertificateNumber { get; set; }

        public Guid VehicleTypeId { get; set; }

        public string VehicleTypeName { get; set; }

        public Guid StoreId { get; set; }
        
        public string StoreName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Vehicle, VehicleLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.RegistrationNumber, opt =>
                    opt.MapFrom(src => src.RegistrationNumber))
                .ForMember(dst => dst.RegistrationCertificateNumber, opt =>
                    opt.MapFrom(src => src.RegistrationCertificateNumber))
                .ForMember(dst => dst.VehicleTypeId, opt =>
                    opt.MapFrom(src => src.VehicleTypeId))
                .ForMember(dst => dst.VehicleTypeName, opt =>
                    opt.MapFrom(src => src.VehicleType.Name))
                .ForMember(dst => dst.StoreId, opt =>
                    opt.MapFrom(src => src.StoreId))
                .ForMember(dst => dst.StoreName, opt =>
                    opt.MapFrom(src => src.Store.Name));
        }
    }
}
