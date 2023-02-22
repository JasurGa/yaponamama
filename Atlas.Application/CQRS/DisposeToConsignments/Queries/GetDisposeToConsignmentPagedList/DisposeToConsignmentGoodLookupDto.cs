using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.DisposeToConsignments.Queries.GetDisposeToConsignmentPagedList
{
    public class DisposeToConsignmentGoodLookupDto : IMapWith<Good>
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string ProviderName { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Good, DisposeToConsignmentGoodLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ProviderId, opt =>
                    opt.MapFrom(src => src.Provider.Id))
                .ForMember(dst => dst.ProviderName, opt =>
                    opt.MapFrom(src => src.Provider.Name))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.PhotoPath, opt =>
                    opt.MapFrom(src => src.PhotoPath));
        }
    }
}
