using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class GoodInCategoryLookupDto : IMapWith<Good>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public float Discount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Good, GoodInCategoryLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath))
                .ForMember(dest => dest.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dest => dest.Discount, opt =>
                    opt.MapFrom(src => src.Discount));
        }
    }
}
