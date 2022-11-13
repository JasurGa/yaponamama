using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryDetails;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryList
{
    public class PromoCategoryLookupDto : IMapWith<PromoCategory>
    {
        public Guid Id { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PromoCategory, PromoCategoryLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.NameRu, opt =>
                    opt.MapFrom(src => src.NameRu))
                .ForMember(dst => dst.NameEn, opt =>
                    opt.MapFrom(src => src.NameEn))
                .ForMember(dst => dst.NameUz, opt =>
                    opt.MapFrom(src => src.NameUz))
                .ForMember(dst => dst.ImageUrl, opt =>
                    opt.MapFrom(src => src.ImageUrl));
        }
    }
}