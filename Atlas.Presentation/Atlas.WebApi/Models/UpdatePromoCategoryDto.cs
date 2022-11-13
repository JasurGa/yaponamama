using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoCategories.Commands.UpdatePromoCategory;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdatePromoCategoryDto : IMapWith<UpdatePromoCategoryCommand>
    {
        public Guid Id { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePromoCategoryDto, UpdatePromoCategoryCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.NameRu, opt =>
                    opt.MapFrom(x => x.NameRu))
                .ForMember(x => x.NameEn, opt =>
                    opt.MapFrom(x => x.NameEn))
                .ForMember(x => x.NameUz, opt =>
                    opt.MapFrom(x => x.NameUz))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x => x.ImageUrl));
        }
    }
}

