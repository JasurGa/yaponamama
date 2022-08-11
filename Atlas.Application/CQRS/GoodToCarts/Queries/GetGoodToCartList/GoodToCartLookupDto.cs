using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GoodToCartLookupDto : IMapWith<GoodToCart>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public Guid ClientId { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GoodToCart, GoodToCartLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
