using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Categories.Queries.SearchCategoriesByGoodName
{
    public class SearchedCategoryLookupDto : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, SearchedCategoryLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dst => dst.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dst => dst.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz));
        }
    }
}

