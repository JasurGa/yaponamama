using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.SupplyManagers.Commands.CreateSupplyManager;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateSupplyManagerDto : IMapWith<CreateSupplyManagerCommand>
    {
        public Guid UserId { get; set; }

        public Guid StoreId { get; set; }

        public string PhoneNumber { get; set; }

        public string PassportPhotoPath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSupplyManagerDto, CreateSupplyManagerCommand>()
                .ForMember(x => x.UserId, opt =>
                    opt.MapFrom(x => x.UserId))
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(x => x.StoreId))
                .ForMember(x => x.PhoneNumber, opt =>
                    opt.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.PassportPhotoPath, opt =>
                    opt.MapFrom(x => x.PassportPhotoPath));
        }
    }
}
