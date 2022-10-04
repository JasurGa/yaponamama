using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Goods.Commands.DiscountGoods;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class DiscountGoodsDto : IMapWith<DiscountGoodsCommand>
    {
        public float Discount { get; set; }

        public IList<Guid> GoodIds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiscountGoodsDto, DiscountGoodsCommand>()
                .ForMember(x => x.GoodIds, opt =>
                    opt.MapFrom(x => x.GoodIds))
                .ForMember(x => x.Discount, opt =>
                    opt.MapFrom(x => x.Discount));
        }
    }
}
