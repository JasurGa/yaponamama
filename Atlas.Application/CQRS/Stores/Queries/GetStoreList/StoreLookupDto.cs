using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class StoreLookupDto : IMapWith<Store>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude))
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber));
        }
    }
}
