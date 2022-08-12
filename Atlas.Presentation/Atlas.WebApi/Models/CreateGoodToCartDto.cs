using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToCarts.Commands.CreateGoodToCart;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateGoodToCartDto : IMapWith<CreateGoodToCartCommand>
    {
        public Guid GoodId { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGoodToCartDto, CreateGoodToCartCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
