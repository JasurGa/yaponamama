using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberDetails
{
    public class ProviderPhoneNumberDetailsVm : IMapWith<ProviderPhoneNumber>
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProviderPhoneNumber, ProviderPhoneNumberDetailsVm>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber));
        }
    }
}