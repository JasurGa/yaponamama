using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.PromoUsages.Queries.GetPromoUsagesByClientId
{
    public class PromoUsageLookupDto : IMapWith<PromoUsage>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid PromoId { get; set; }

        public DateTime UsedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PromoUsage, PromoUsageLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.PromoId, opt =>
                    opt.MapFrom(src => src.PromoId))
                .ForMember(dst => dst.UsedAt, opt =>
                    opt.MapFrom(src => src.UsedAt));
        }
    }
}