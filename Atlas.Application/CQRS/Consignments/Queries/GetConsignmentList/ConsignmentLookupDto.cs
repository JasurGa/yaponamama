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
                .ForMember(eVm => eVm.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(eVm => eVm.StoreToGoodId,
                    opt => opt.MapFrom(e => e.StoreToGoodId))
                .ForMember(eVm => eVm.PurchasedAt,
                    opt => opt.MapFrom(e => e.PurchasedAt))
                .ForMember(eVm => eVm.ExpirateAt,
                    opt => opt.MapFrom(e => e.ExpirateAt))
                .ForMember(eVm => eVm.ShelfLocation,
                    opt => opt.MapFrom(e => e.ShelfLocation));
        }
    }
}
