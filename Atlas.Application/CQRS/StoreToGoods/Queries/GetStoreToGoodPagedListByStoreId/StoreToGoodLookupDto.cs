using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodPagedListByStoreId
{
    public class StoreToGoodLookupDto : IMapWith<StoreToGood>
    {
        public Guid Id { get; set; }

        public long Count { get; set; }

        public string Name { get; set; }

        public string PhotoPath { get; set; }

        public long SellingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreToGood, StoreToGoodLookupDto>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Count, opt =>
                    opt.MapFrom(x => x.Count))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Good.Name))
                .ForMember(x => x.PhotoPath, opt =>
                    opt.MapFrom(x => x.Good.PhotoPath))
                .ForMember(x => x.SellingPrice, opt =>
                    opt.MapFrom(x => x.Good.SellingPrice))
                .ForMember(x => x.PurchasePrice, opt =>
                    opt.MapFrom(x => x.Good.PurchasePrice))
                .ForMember(x => x.ProviderId, opt =>
                    opt.MapFrom(x => x.Good.ProviderId));
        }
    }
}