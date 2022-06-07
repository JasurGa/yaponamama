using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumber;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateProviderPhoneNumberDto : IMapWith<CreateProviderPhoneNumberCommand>
    {
        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProviderPhoneNumberDto, CreateProviderPhoneNumberCommand>()
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber));
        }
    }
}
