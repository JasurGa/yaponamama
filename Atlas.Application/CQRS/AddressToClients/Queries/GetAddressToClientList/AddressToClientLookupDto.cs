using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList
{
    public class AddressToClientLookupDto : IMapWith<AddressToClient>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public int AddressType { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressToClient, AddressToClientLookupDto>()
                .ForMember(eVm => eVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eVm => eVm.Address,
                    opt => opt.MapFrom(e => e.Address))
                .ForMember(eVm => eVm.Entrance,
                    opt => opt.MapFrom(e => e.Entrance))
                .ForMember(eVm => eVm.Floor,
                    opt => opt.MapFrom(e => e.Floor))
                .ForMember(eVm => eVm.Apartment,
                    opt => opt.MapFrom(e => e.Apartment))
                .ForMember(eVm => eVm.ClientId,
                    opt => opt.MapFrom(e => e.ClientId))
                .ForMember(eVm => eVm.Latitude,
                    opt => opt.MapFrom(e => e.Latitude))
                .ForMember(eVm => eVm.Longitude,
                    opt => opt.MapFrom(e => e.Longitude))
                .ForMember(eVm => eVm.AddressType,
                    opt => opt.MapFrom(e => e.AddressType))
                .ForMember(eVm => eVm.PhoneNumber, 
                    opt => opt.MapFrom(e => e.PhoneNumber))
                .ForMember(eVm => eVm.CreatedAt,
                    opt => opt.MapFrom(e => e.CreatedAt));
        }
    }
}
