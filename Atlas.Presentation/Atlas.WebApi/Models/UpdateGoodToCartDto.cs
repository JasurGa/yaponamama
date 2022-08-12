using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToCarts.Commands.UpdateGoodToCart;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateGoodToCartDto : IMapWith<UpdateGoodToCartCommand>
    {
        public Guid Id { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGoodToCartDto, UpdateGoodToCartCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
