using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateStoreToGoodDto : IMapWith<CreateStoreToGoodCommand>
    {
        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public long Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStoreToGoodDto, CreateStoreToGoodCommand>()
                .ForMember(p => p.StoreId, opt =>
                    opt.MapFrom(p => p.StoreId))
                .ForMember(p => p.GoodId, opt =>
                    opt.MapFrom(p => p.GoodId))
                .ForMember(p => p.Count, opt =>
                    opt.MapFrom(p => p.Count));
        }
    }
}
