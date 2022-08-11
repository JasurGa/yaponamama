using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Consignments.Commands.CreateConsignment;
using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateConsignmentDto : IMapWith<CreateConsignmentCommand>
    {
        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public CreateStoreToGoodDto StoreToGood { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateConsignmentDto, CreateConsignmentCommand>()
                .ForMember(x => x.StoreToGood, opt =>
                    opt.MapFrom(x => x.StoreToGood))
                .ForMember(x => x.PurchasedAt, opt =>
                    opt.MapFrom(x => x.PurchasedAt))
                .ForMember(x => x.ExpirateAt, opt =>
                    opt.MapFrom(x => x.ExpirateAt))
                .ForMember(x => x.ShelfLocation, opt =>
                    opt.MapFrom(x => x.ShelfLocation));
        }
    }
}
