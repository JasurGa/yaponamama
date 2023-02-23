using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Linq;

namespace Atlas.Application.CQRS.GoodToCarts.Queries.GetGoodToCartList
{
    public class GoodToCartLookupDto : IMapWith<GoodToCart>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string GoodName { get; set; }

        public string GoodPhotoPath { get; set; }

        public long GoodPrice { get; set; }

        public float GoodDiscount { get; set; }

        public Guid ClientId { get; set; }

        public int Count { get; set; }

        public int MaxCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GoodToCart, GoodToCartLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.GoodName, opt =>
                    opt.MapFrom(src => src.Good.Name))
                .ForMember(dst => dst.GoodPhotoPath, opt =>
                    opt.MapFrom(src => src.Good.PhotoPath))
                .ForMember(dst => dst.GoodPrice, opt =>
                    opt.MapFrom(src => src.Good.SellingPrice))
                .ForMember(dst => dst.GoodDiscount, opt =>
                    opt.MapFrom(src => src.Good.Discount))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count))
                .ForMember(dst => dst.MaxCount, opt =>
                    opt.MapFrom(src => src.Good.StoreToGoods.Sum(x => x.Count)));
        }
    }
}
