using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.UpdateProviderPhoneNumber;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateProviderPhoneNumberDto : IMapWith<UpdateProviderPhoneNumberCommand>
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProviderPhoneNumberDto, UpdateProviderPhoneNumberCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.ProviderId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber));
        }
    }
}
