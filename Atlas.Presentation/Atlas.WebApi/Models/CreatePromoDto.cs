﻿using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Promos.Commands.CreatePromo;
using AutoMapper;

namespace Atlas.WebApi.Models
{

    public class CreatePromoDto : IMapWith<CreatePromoCommand>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? ClientId { get; set; }

        public int DiscountPrice { get; set; }

        public float DiscountPercent { get; set; }

        public bool ForAllGoods { get; set; }

        public bool FreeDelivery { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoDto, CreatePromoCommand>()
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt =>
                    opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.DiscountPrice, opt =>
                    opt.MapFrom(src => src.DiscountPrice))
                .ForMember(dst => dst.DiscountPercent, opt =>
                    opt.MapFrom(src => src.DiscountPercent))
                .ForMember(dst => dst.FreeDelivery, opt =>
                    opt.MapFrom(src => src.FreeDelivery))
                .ForMember(dst => dst.ForAllGoods, opt =>
                    opt.MapFrom(src => src.ForAllGoods))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}
