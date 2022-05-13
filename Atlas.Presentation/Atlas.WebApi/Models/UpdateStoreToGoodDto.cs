using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Linq;

namespace Atlas.WebApi.Models
{
    public class UpdateStoreToGoodDto : IMapWith<StoreToGood>
    {
        public Guid Id { get; set; }

        public long Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStoreToGoodDto, UpdateStoreToGoodCommand>()
                .ForMember(p => p.Id, opt =>
                    opt.MapFrom(p => p.Id))
                .ForMember(p => p.Count, opt =>
                    opt.MapFrom(p => p.Count));
        }
    }
}
