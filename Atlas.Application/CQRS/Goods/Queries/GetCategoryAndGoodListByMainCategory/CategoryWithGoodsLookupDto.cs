using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class CategoryWithGoodsLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }


        public int GoodsCount { get; set; }

        public List<GoodInCategoryLookupDto> Goods { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryWithGoodsLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dest => dest.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz))
                .ForMember(dest => dest.GoodsCount, opt =>
                    opt.MapFrom(src => src.GoodsCount));
        }

    }
}
