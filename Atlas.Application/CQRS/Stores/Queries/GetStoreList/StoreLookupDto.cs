using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class StoreLookupDto : IMapWith<Store>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreLookupDto>()
                .ForMember(eVm => eVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eVm => eVm.Name,
                    opt => opt.MapFrom(e => e.Name))
                .ForMember(eVm => eVm.Latitude,
                    opt => opt.MapFrom(e => e.Latitude))
                .ForMember(eVm => eVm.Longitude,
                    opt => opt.MapFrom(e => e.Longitude))
                .ForMember(eVm => eVm.Address,
                    opt => opt.MapFrom(e => e.Address));
        }
    }
}
