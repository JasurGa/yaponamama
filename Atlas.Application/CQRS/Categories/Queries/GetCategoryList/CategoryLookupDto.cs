using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryList
{
    public class CategoryLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public int ChildCategoriesCount { get; set; }
            
        public int TotalAvailableGoodsCount { get; set; }

        public int GoodsCount { get; set; }
        
        public int OrderNumber { get; set; }

        public bool IsHidden { get; set; }

        public List<Guid> Children { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dst => dst.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dst => dst.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz))
                .ForMember(dst => dst.ImageUrl, opt =>
                    opt.MapFrom(src => src.ImageUrl))
                .ForMember(dst => dst.IsMainCategory, opt =>
                    opt.MapFrom(src => src.IsMainCategory))
                .ForMember(dst => dst.ChildCategoriesCount, opt =>
                    opt.MapFrom(src => src.ChildCategoriesCount))
                .ForMember(dst => dst.GoodsCount, opt =>
                    opt.MapFrom(src => src.GoodsCount))
                .ForMember(dst => dst.OrderNumber, opt =>
                    opt.MapFrom(src => src.OrderNumber))
                .ForMember(dst => dst.IsHidden, opt =>
                    opt.MapFrom(src => src.IsHidden));
        }
    }
}
