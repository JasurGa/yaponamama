using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using System;
using AutoMapper;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoList
{
    public class PromoLookupDto : IMapWith<Promo>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DiscountPrice { get; set; }

        public float DiscountPercent { get; set; }

        public bool ForAllGoods { get; set; }

        public bool FreeDelivery { get; set; }

        public DateTime ExpiresAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Promo, PromoLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt =>
                    opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.DiscountPrice, opt =>
                    opt.MapFrom(src => src.DiscountPrice))
                .ForMember(dst => dst.DiscountPercent, opt =>
                    opt.MapFrom(src => src.DiscountPercent))
                .ForMember(dst => dst.ForAllGoods, opt =>
                    opt.MapFrom(src => src.ForAllGoods))
                .ForMember(dst => dst.FreeDelivery, opt =>
                    opt.MapFrom(src => src.FreeDelivery))
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}
