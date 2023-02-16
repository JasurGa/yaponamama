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
                .ForMember(dst => dst.ExpiresAt, opt =>
                    opt.MapFrom(src => src.ExpiresAt));
        }
    }
}
