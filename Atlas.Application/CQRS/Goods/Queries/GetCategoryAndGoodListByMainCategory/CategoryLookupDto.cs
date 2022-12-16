using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Goods.Queries.GetCategoryAndGoodListByMainCategory
{
    public class CategoryLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int GoodsCount { get; set; }

        public List<GoodLookupDto> Goods { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GoodsCount, opt =>
                    opt.MapFrom(src => src.GoodsCount));
        }

    }
}
