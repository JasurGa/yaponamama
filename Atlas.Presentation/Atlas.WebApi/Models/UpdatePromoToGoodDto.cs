using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.PromoToGoods.Commands.CreatePromoToGood;
using Atlas.Application.CQRS.PromoToGoods.Commands.UpdatePromoToGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdatePromoToGoodDto : IMapWith<UpdatePromoToGoodCommand>
    {
        public Guid Id { get; set; }

        public Guid PromoId { get; set; }

        public Guid GoodId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePromoToGoodDto, UpdatePromoToGoodCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.PromoId, opt =>
                    opt.MapFrom(src => src.PromoId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId));
        }
    }
}
