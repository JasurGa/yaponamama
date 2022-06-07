using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoDetails
{
    public class PromoDetailsVm : IMapWith<Promo>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string Name { get; set; }

        public int DiscountPrice { get; set; }

        public int DiscountPercent { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Promo, PromoDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.DiscountPrice, opt =>
                    opt.MapFrom(src => src.DiscountPrice))
                .ForMember(dst => dst.DiscountPercent, opt =>
                    opt.MapFrom(src => src.DiscountPercent));
        }
    }
}
