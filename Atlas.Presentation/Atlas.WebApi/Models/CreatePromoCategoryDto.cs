using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategories.Commands.CreatePromoCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePromoCategoryDto : IMapWith<CreatePromoCategoryCommand>
    {
        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoCategoryDto, CreatePromoCategoryCommand>()
                .ForMember(p => p.NameRu, opt =>
                    opt.MapFrom(p => p.NameRu))
                .ForMember(p => p.NameEn, opt =>
                    opt.MapFrom(p => p.NameEn))
                .ForMember(p => p.NameUz, opt =>
                    opt.MapFrom(p => p.NameUz))
                .ForMember(p => p.ImageUrl, opt =>
                    opt.MapFrom(p => p.ImageUrl));
        }
    }
}

