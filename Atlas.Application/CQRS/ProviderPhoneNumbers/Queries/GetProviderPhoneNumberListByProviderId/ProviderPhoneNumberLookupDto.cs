using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId
{
    public class ProviderPhoneNumberLookupDto : IMapWith<ProviderPhoneNumber>
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProviderPhoneNumber, ProviderPhoneNumberLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ProviderId, opt =>
                    opt.MapFrom(src => src.ProviderId))
                .ForMember(dst => dst.PhoneNumber, opt =>
                    opt.MapFrom(src => src.PhoneNumber));
        }
    }
}