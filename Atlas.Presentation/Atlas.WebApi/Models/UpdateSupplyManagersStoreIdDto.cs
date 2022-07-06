using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.SupplyManagers.Commands.UpdateSupplyManagersStoreId;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateSupplyManagersStoreIdDto : IMapWith<UpdateSupplyManagersStoreIdCommand>
    {
        public Guid StoreId { get; set; }

        public List<Guid> SupplyManagerIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSupplyManagersStoreIdDto, UpdateSupplyManagersStoreIdCommand>()
                .ForMember(dst => dst.StoreId, opt => opt.MapFrom(src =>
                    src.StoreId))
                .ForMember(dst => dst.SupplyManagersId, opt => opt.MapFrom(src =>
                    src.SupplyManagerIds));
        }
    }
}
