using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Vehicles.Commands.UpdateVehiclesStoreId;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateVehiclesStoreIdDto : IMapWith<UpdateVehiclesStoreIdCommand>
    {
        public Guid StoreId { get; set; }

        public List<Guid> VehicleIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateVehiclesStoreIdDto, UpdateVehiclesStoreIdCommand>()
                .ForMember(dst => dst.StoreId, opt => opt.MapFrom(src =>
                    src.StoreId))
                .ForMember(dst => dst.VehicleIds, opt => opt.MapFrom(src =>
                    src.VehicleIds));
        }
    }
}
