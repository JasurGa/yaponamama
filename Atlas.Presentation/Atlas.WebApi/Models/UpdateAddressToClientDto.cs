using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.AddressToClients.Commands.UpdateAddressToClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateAddressToClientDto : IMapWith<UpdateAddressToClientCommand>
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateAddressToClientDto, UpdateAddressToClientCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(src => src.Address))
                .ForMember(x => x.Entrance, opt =>
                    opt.MapFrom(src => src.Entrance))
                .ForMember(x => x.Floor, opt =>
                    opt.MapFrom(src => src.Floor))
                .ForMember(x => x.Apartment, opt =>
                    opt.MapFrom(src => src.Apartment))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(src => src.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(src => src.Longitude));
        }
    }
}
