using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Promos.Commands.CreatePromo;
using AutoMapper;

namespace Atlas.WebApi.Models
{

    public class CreatePromoDto : IMapWith<CreatePromoCommand>
    {
        public string Name { get; set; }

        public Guid GoodId { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePromoDto, CreatePromoCommand>()
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.DiscountPrice, opt =>
                    opt.MapFrom(src => src.DiscountPrice))
                .ForMember(dst => dst.DiscountPercent, opt =>
                    opt.MapFrom(src => src.DiscountPercent))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}
