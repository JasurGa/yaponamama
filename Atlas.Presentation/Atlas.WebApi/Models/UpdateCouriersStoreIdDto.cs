using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCouriersStoreId;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateCouriersStoreIdDto : IMapWith<UpdateCouriersStoreIdCommand>
    {
        public Guid StoreId { get; set; }

        public List<Guid> CourierIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCouriersStoreIdDto, UpdateCouriersStoreIdCommand>()
                .ForMember(dst => dst.StoreId, opt => opt.MapFrom(src =>
                    src.StoreId))
                .ForMember(dst => dst.CourierIds, opt => opt.MapFrom(src =>
                    src.CourierIds));
        }
    }
}
