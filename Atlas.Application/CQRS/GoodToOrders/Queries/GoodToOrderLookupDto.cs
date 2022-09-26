using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Queries
{
    public class GoodToOrderLookupDto : IMapWith<GoodToOrder>
    {
        public Guid Id { get; set; }

        public Guid GoodId { get; set; }

        public string GoodName { get; set; }

        public string GoodImagePath { get; set; }

        public long GoodSellingPrice { get; set; }

        public float GoodDiscount { get; set; }

        public long GoodPurchasePrice { get; set; }

        public Guid ProviderId { get; set; }

        public string ProviderName { get; set; }

        public Guid OrderId { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GoodToOrder, GoodToOrderLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dest => dest.GoodName, opt =>
                    opt.MapFrom(src => src.Good.Name))
                .ForMember(dest => dest.GoodImagePath, opt =>
                    opt.MapFrom(src => src.Good.PhotoPath))
                .ForMember(dest => dest.GoodSellingPrice, opt =>
                    opt.MapFrom(src => src.Good.SellingPrice))
                .ForMember(dest => dest.GoodDiscount, opt =>
                    opt.MapFrom(src => src.Good.Discount))
                .ForMember(dest => dest.OrderId, opt =>
                    opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Count, opt =>
                    opt.MapFrom(src => src.Count))
                .ForMember(dest => dest.GoodPurchasePrice, opt =>
                    opt.MapFrom(src => src.Good.PurchasePrice))
                .ForMember(dest => dest.ProviderId, opt =>
                    opt.MapFrom(src => src.Good.Provider.Id))
                .ForMember(dest => dest.ProviderName, opt =>
                    opt.MapFrom(src => src.Good.Provider.Name));
        }
    }
}
