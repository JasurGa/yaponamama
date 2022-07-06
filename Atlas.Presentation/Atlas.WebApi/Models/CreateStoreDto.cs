using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Stores.Commands.CreateStore;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateStoreDto : IMapWith<CreateStoreCommand>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStoreDto, CreateStoreCommand>()
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name))
                .ForMember(x => x.Address, opt =>
                    opt.MapFrom(x => x.Address))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Latitude, opt =>
                    opt.MapFrom(x => x.Latitude))
                .ForMember(x => x.Longitude, opt =>
                    opt.MapFrom(x => x.Longitude));
        }
    }
}
