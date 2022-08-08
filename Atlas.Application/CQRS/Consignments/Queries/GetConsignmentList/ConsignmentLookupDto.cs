using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class ConsignmentLookupDto : IMapWith<Consignment>
    {
        public Guid Id { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public string GoodName { get; set; }

        public string GoodImagePath { get; set; }

        public long GoodCount { get; set; }

        public string StoreName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consignment, ConsignmentLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.PurchasedAt, opt =>
                    opt.MapFrom(x => x.PurchasedAt))
                .ForMember(x => x.ExpirateAt, opt =>
                    opt.MapFrom(x => x.ExpirateAt))
                .ForMember(x => x.ShelfLocation, opt =>
                    opt.MapFrom(x => x.ShelfLocation))
                .ForMember(x => x.GoodName, opt =>
                    opt.MapFrom(x => x.StoreToGood.Good.Name))
                .ForMember(x => x.GoodImagePath, opt =>
                    opt.MapFrom(x => x.StoreToGood.Good.PhotoPath))
                .ForMember(x => x.GoodCount, opt =>
                    opt.MapFrom(x => x.StoreToGood.Count))
                .ForMember(x => x.StoreName, opt =>
                    opt.MapFrom(x => x.StoreToGood.Store.Name));
        }
    }
}
