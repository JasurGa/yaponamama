using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class BotOrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public string ExternalId { get; set; }

        public long TotalPrice { get; set; }
        
        public long SellingPriceDiscount { get; set; }

        public long ShippingPriceDiscount { get; set; }

        public bool IsPickup { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeliverAt { get; set; }

        public DateTime? StatusLastEditedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public bool IsPrivateHouse { get; set; }

        public List<BotGoodToOrderLookupDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, BotOrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ExternalId, opt =>
                    opt.MapFrom(src => src.ExternalId))
                .ForMember(dst => dst.TotalPrice, opt =>
                    opt.MapFrom(src => (long)(Math.Ceiling(src.SellingPrice) + Math.Ceiling(src.ShippingPrice))))
                .ForMember(dst => dst.SellingPriceDiscount, opt =>
                    opt.MapFrom(src => src.SellingPriceDiscount))
                .ForMember(dst => dst.ShippingPriceDiscount, opt =>
                    opt.MapFrom(src => src.ShippingPriceDiscount))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.StatusLastEditedAt, opt =>
                    opt.MapFrom(src => src.StatusLastEditedAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.IsPrivateHouse, opt =>
                    opt.MapFrom(src => src.IsPrivateHouse))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}
