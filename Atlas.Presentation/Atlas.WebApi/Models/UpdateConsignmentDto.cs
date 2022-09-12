using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Consignments.Commands.UpdateConsignment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateConsignmentDto : IMapWith<UpdateConsignmentCommand>
    {
        public Guid Id { get; set; }

        public Guid StoreId { get; set; }

        public Guid GoodId { get; set; }

        public int Count { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateConsignmentDto, UpdateConsignmentCommand>()
                .ForMember(p => p.Id, opt =>
                    opt.MapFrom(p => p.Id))
                .ForMember(p => p.StoreId, opt =>
                    opt.MapFrom(p => p.StoreId))
                .ForMember(p => p.GoodId, opt =>
                    opt.MapFrom(p => p.GoodId))
                .ForMember(p => p.PurchasedAt, opt =>
                    opt.MapFrom(p => p.PurchasedAt))
                .ForMember(p => p.ExpirateAt, opt =>
                    opt.MapFrom(p => p.ExpirateAt))
                .ForMember(p => p.ShelfLocation, opt =>
                    opt.MapFrom(p => p.ShelfLocation))
                .ForMember(p => p.Count, opt =>
                    opt.MapFrom(p => p.Count));
        }
    }
}
