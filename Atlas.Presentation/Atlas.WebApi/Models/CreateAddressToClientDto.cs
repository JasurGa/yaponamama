using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateAddressToClientDto : IMapWith<CreateAddressToClientCommand>
    {
        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAddressToClientDto, CreateAddressToClientCommand>()
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.Entrance, opt =>
                    opt.MapFrom(x => x.Entrance))
                .ForMember(x => x.Floor, opt =>
                    opt.MapFrom(x => x.Floor))
                .ForMember(x => x.Apartment, opt =>
                    opt.MapFrom(x => x.Apartment))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude));
        }
    }
}
