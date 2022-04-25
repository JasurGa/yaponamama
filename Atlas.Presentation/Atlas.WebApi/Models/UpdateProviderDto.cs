using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Providers.Commands.UpdateProvider;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.WebApi.Models
{
    public class UpdateProviderDto : IMapWith<UpdateProviderCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string LogotypePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProviderDto, UpdateProviderCommand>()
                .ForMember(p => p.Id, opt =>
                    opt.MapFrom(p => p.Id))
                .ForMember(p => p.Name, opt =>
                    opt.MapFrom(p => p.Name))
                .ForMember(p => p.Longitude, opt =>
                    opt.MapFrom(p => p.Longitude))
                .ForMember(p => p.Latitude, opt =>
                    opt.MapFrom(p => p.Latitude))
                .ForMember(p => p.Address, opt =>
                    opt.MapFrom(p => p.Address))
                .ForMember(p => p.Description, opt =>
                    opt.MapFrom(p => p.Description))
                .ForMember(p => p.LogotypePath, opt =>
                    opt.MapFrom(p => p.LogotypePath));
        }
    }
}
