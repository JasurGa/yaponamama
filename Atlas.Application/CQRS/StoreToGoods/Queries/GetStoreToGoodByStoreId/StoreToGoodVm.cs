using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId
{
    public class StoreToGoodVm : IMapWith<StoreToGood>
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public long Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreToGood, StoreToGoodVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.StoreId, opt =>
                    opt.MapFrom(src => src.StoreId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
