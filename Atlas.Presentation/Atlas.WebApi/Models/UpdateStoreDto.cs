using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Stores.Commands.UpdateStore;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateStoreDto : IMapWith<UpdateStoreCommand>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStoreDto, UpdateStoreCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude))
                .ForMember(x => x.IsDeleted, opt =>
                    opt.MapFrom(x => x.IsDeleted));
        }
    }
}
