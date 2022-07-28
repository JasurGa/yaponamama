using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumbers;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateProviderPhoneNumbersDto : IMapWith<CreateProviderPhoneNumbersCommand>
    {
        public Guid ProviderId { get; set; }

        public List<string> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProviderPhoneNumbersDto, CreateProviderPhoneNumbersCommand>()
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.PhoneNumbers, opt =>
                    opt.MapFrom(x => x.PhoneNumbers));
        }
    }
}
