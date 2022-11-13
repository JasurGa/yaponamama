using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.CreatePromoAdvertiseGood;
using Atlas.Application.CQRS.PromoAdvertiseGoods.Commands.DeletePromoAdvertiseGood;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class DeletePromoAdvertiseGoodDto : IMapWith<DeletePromoAdvertiseGoodCommand>
    {
        public Guid GoodId { get; set; }

        public Guid PromoAdvertisePageId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeletePromoAdvertiseGoodDto, DeletePromoAdvertiseGoodCommand>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.PromoAdvertisePageId, opt =>
                    opt.MapFrom(src => src.PromoAdvertisePageId));
        }
    }
}

