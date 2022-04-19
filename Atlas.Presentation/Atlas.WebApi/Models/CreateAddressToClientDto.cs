using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateAddressToClientDto : IMapWith<CreateAddressToClientCommand>
    {
        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAddressToClientDto, CreateAddressToClientCommand>()
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude));
        }
    }
}
