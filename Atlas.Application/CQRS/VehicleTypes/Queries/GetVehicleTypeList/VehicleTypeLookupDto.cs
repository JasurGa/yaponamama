using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.VehicleTypes.Queries.GetVehicleTypeList
{
    public class VehicleTypeLookupDto : IMapWith<VehicleType>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VehicleType, VehicleTypeLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.NameRu, opt =>
                    opt.MapFrom(x => x.NameRu))
                .ForMember(x => x.NameEn, opt =>
                    opt.MapFrom(x => x.NameEn))
                .ForMember(x => x.NameUz, opt =>
                    opt.MapFrom(x => x.NameUz));
        }
    }
}