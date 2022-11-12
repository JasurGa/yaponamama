﻿using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoAdvertises.Commands.CreatePromoAdvertise;
using Atlas.Application.CQRS.PromoAdvertises.Commands.UpdatePromoAdvertise;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdatePromoAdvertiseDto : IMapWith<UpdatePromoAdvertiseCommand>
    {
        public Guid Id { get; set; }

        public string WideBackground { get; set; }

        public string HighBackground { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public int OrderNumber { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePromoAdvertiseDto, UpdatePromoAdvertiseCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.WideBackground, opt =>
                    opt.MapFrom(src => src.WideBackground))
                .ForMember(dst => dst.HighBackground, opt =>
                    opt.MapFrom(src => src.HighBackground))
                .ForMember(dst => dst.TitleColor, opt =>
                    opt.MapFrom(src => src.TitleColor))
                .ForMember(dst => dst.TitleRu, opt =>
                    opt.MapFrom(src => src.TitleRu))
                .ForMember(dst => dst.TitleEn, opt =>
                    opt.MapFrom(src => src.TitleEn))
                .ForMember(dst => dst.TitleUz, opt =>
                    opt.MapFrom(src => src.TitleUz))
                .ForMember(dst => dst.OrderNumber, opt =>
                    opt.MapFrom(src => src.OrderNumber))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}

