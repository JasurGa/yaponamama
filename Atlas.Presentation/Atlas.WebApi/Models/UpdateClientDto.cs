using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Commands.UpdateClient;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateClientDto : IMapWith<UpdateClientCommand>
    {
        public Guid Id { get; set; }

        public string PhoneNumber { get; set; }

        public object VehicleId { get; internal set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClientDto, UpdateClientCommand>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        }
    }
}
