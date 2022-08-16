using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails
{
    public class CategoryDetailsVm : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryDetailsVm>()
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
                    opt.MapFrom(src => src.IsMainCategory));
        }
    }
}
