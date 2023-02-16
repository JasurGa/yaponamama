using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreatePromoToGoodDto : IMapWith<CreatePromoToGoodCommand>
    {
        public Guid PromoId { get; set; }

        public Guid GoodId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoToGoodDto, CreatePromoToGoodCommand>()
                .ForMember(dst => dst.PromoId, opt =>
                    opt.MapFrom(src => src.PromoId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId));
        }
    }
}
