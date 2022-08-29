using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentDetails
{
    public class ConsignmentDetailsVm : IMapWith<Consignment>
    {
        public Guid Id { get; set; }

        public Guid StoreToGoodId { get; set; }

        public StoreLookupDto Store { get; set; }

        public GoodLookupDto Good { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consignment, ConsignmentDetailsVm>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.StoreToGoodId, opt =>
                    opt.MapFrom(x => x.StoreToGoodId))
                .ForMember(x => x.Store, opt =>
                    opt.MapFrom(x => x.StoreToGood.Store))
                .ForMember(x => x.Good, opt =>
                    opt.MapFrom(x => x.StoreToGood.Good))
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
