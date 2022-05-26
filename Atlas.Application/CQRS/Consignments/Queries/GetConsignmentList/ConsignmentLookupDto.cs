using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.Consignments.Queries.GetConsignmentList
{
    public class ConsignmentLookupDto : IMapWith<Consignment>
    {
        public Guid Id { get; set; }

        public Guid StoreToGoodId { get; set; }

        public DateTime PurchasedAt { get; set; }

        public DateTime ExpirateAt { get; set; }

        public string ShelfLocation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consignment, ConsignmentLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.StoreToGoodId, opt =>
                    opt.MapFrom(x => x.StoreToGoodId))
                .ForMember(x => x.PurchasedAt, opt =>
                    opt.MapFrom(x => x.PurchasedAt))
                .ForMember(x => x.ExpirateAt, opt =>
                    opt.MapFrom(x => x.ExpirateAt))
                .ForMember(x => x.ShelfLocation, opt =>
                    opt.MapFrom(x => x.ShelfLocation));
        }
    }
}
