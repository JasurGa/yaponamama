using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoAdvertisePages.Commands.CreatePromoAdvertisePage;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreatePromoAdvertisePageDto : IMapWith<CreatePromoAdvertisePageCommand>
    {
        public Guid PromoAdvertiseId { get; set; }

        public string BadgeColor { get; set; }

        public string BadgeTextRu { get; set; }

        public string BadgeTextEn { get; set; }

        public string BadgeTextUz { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public string DescriptionColor { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string ButtonColor { get; set; }

        public string Background { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoAdvertisePageDto, CreatePromoAdvertisePageCommand>()
                .ForMember(dst => dst.PromoAdvertiseId, opt =>
                    opt.MapFrom(src => src.PromoAdvertiseId))
                .ForMember(dst => dst.BadgeColor, opt =>
                    opt.MapFrom(src => src.BadgeColor))
                .ForMember(dst => dst.BadgeTextRu, opt =>
                    opt.MapFrom(src => src.BadgeTextRu))
                .ForMember(dst => dst.BadgeTextEn, opt =>
                    opt.MapFrom(src => src.BadgeTextEn))
                .ForMember(dst => dst.BadgeTextUz, opt =>
                    opt.MapFrom(src => src.BadgeTextUz))
                .ForMember(dst => dst.TitleColor, opt =>
                    opt.MapFrom(src => src.TitleColor))
                .ForMember(dst => dst.TitleRu, opt =>
                    opt.MapFrom(src => src.TitleRu))
                .ForMember(dst => dst.TitleEn, opt =>
                    opt.MapFrom(src => src.TitleEn))
                .ForMember(dst => dst.TitleUz, opt =>
                    opt.MapFrom(src => src.TitleUz))
                .ForMember(dst => dst.DescriptionColor, opt =>
                    opt.MapFrom(src => src.DescriptionColor))
                .ForMember(dst => dst.DescriptionRu, opt =>
                    opt.MapFrom(src => src.DescriptionRu))
                .ForMember(dst => dst.DescriptionEn, opt =>
                    opt.MapFrom(src => src.DescriptionEn))
                .ForMember(dst => dst.DescriptionUz, opt =>
                    opt.MapFrom(src => src.DescriptionUz))
                .ForMember(dst => dst.ButtonColor, opt =>
                    opt.MapFrom(src => src.ButtonColor))
                .ForMember(dst => dst.Background, opt =>
                    opt.MapFrom(src => src.Background));
        }
    }
}

