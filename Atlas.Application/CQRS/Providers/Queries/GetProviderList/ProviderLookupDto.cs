using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class ProviderLookupDto : IMapWith<Provider>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string LogotypePath { get; set; }

        public IEnumerable<ProviderPhoneNumber> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Provider, ProviderLookupDto>()
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
                .ForMember(dst => dst.LogotypePath, opt =>
                    opt.MapFrom(src => src.LogotypePath))
                .ForMember(dst => dst.PhoneNumbers, opt =>
                    opt.MapFrom(src => src.ProviderPhoneNumbers));

        }
    }
}
