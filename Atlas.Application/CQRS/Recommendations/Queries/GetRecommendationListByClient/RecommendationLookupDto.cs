using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using System;
using AutoMapper;

namespace Atlas.Application.CQRS.Recommendations.Queries.GetRecommendationListByClient
{
    public class RecommendationLookupDto : IMapWith<Recommendation>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid GoodId { get; set; }

        public Guid RecommendationTypeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Recommendation, RecommendationLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.RecommendationTypeId, opt =>
                    opt.MapFrom(src => src.RecommendationTypeId));
        }
    }
}