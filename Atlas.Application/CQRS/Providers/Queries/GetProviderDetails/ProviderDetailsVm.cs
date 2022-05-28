using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderDetails
{
    public class ProviderDetailsVm : IMapWith<Provider>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string LogotypePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Provider, ProviderDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Longitude, opt =>
                    opt.MapFrom(src => src.Longitude))
                .ForMember(dst => dst.Latitude, opt =>
                    opt.MapFrom(src => src.Latitude))
                .ForMember(dst => dst.Address, opt =>
                    opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.Description, opt =>
                    opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.LogotypePath, opt =>
                    opt.MapFrom(src => src.LogotypePath));
        }
    }
}
