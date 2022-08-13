using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Consignments.Commands.CreateConsignment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateConsignmentDto : IMapWith<CreateConsignmentCommand>
    {
        public Guid GoodId { get; set; }

        public Guid StoreId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateConsignmentDto, CreateConsignmentCommand>()
                .ForMember(x => x.GoodId, opt =>
                    opt.MapFrom(x => x.GoodId))
                .ForMember(x => x.StoreId, opt =>
                    opt.MapFrom(x => x.StoreId))
                .ForMember(x => x.PurchasedAt, opt =>
                    opt.MapFrom(x => x.PurchasedAt))
                .ForMember(x => x.ExpirateAt, opt =>
                    opt.MapFrom(x => x.ExpirateAt))
                .ForMember(x => x.ShelfLocation, opt =>
                    opt.MapFrom(x => x.ShelfLocation))
                .ForMember(x => x.Count, opt =>
                    opt.MapFrom(x => x.Count));
        }
    }
}
