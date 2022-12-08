using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Queries.GetClientDetails;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails;
using Atlas.Application.CQRS.Promos.Queries.GetPromoDetails;
using Atlas.Application.CQRS.Stores.Queries.GetStoreDetails;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class OrderDetailsVm : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public int OrderCode
        {
            get
            {
                return (int)(Convert.ToInt64(Id.ToString().Split("-")[0], 16) % 1000000);
            }
        }

        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public string Apartment { get; set; }

        public string Floor { get; set; }

        public string Entrance { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public float SellingPrice { get; set; }

        public float ShippingPrice { get; set; }

        public float TotalPrice
        {
            get
            {
                return SellingPrice + ShippingPrice;
            }
        }

        public float PurchasePrice { get; set; }

        public int PaymentType { get; set; }

        public int Status { get; set; }

        public bool IsPrePayed { get; set; }

        public DateTime DeliverAt { get; set; }

        public bool CanRefund { get; set; }

        public bool IsRefunded { get; set; }

        public long? TelegramUserId { get; set; }

        public bool IsDevVersionBot { get; set; }

        public int GoodReplacementType { get; set; }

        public StoreDetailsVm Store { get; set; }

        public CourierDetailsVm Courier { get; set; }

        public ClientDetailsVm Client { get; set; }

        public PromoDetailsVm Promo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsVm>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Courier, opt =>
                    opt.MapFrom(src => src.Courier))
                .ForMember(dst => dst.Client, opt =>
                    opt.MapFrom(src => src.Client))
                .ForMember(dst => dst.Promo, opt =>
                    opt.MapFrom(src => src.Promo))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.Store, opt =>
                    opt.MapFrom(src => src.Store))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.DontCallWhenDelivered, opt =>
                    opt.MapFrom(src => src.DontCallWhenDelivered))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dst => dst.ShippingPrice, opt =>
                    opt.MapFrom(src => src.ShippingPrice))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.CanRefund, opt =>
                    opt.MapFrom(src => src.CanRefund))
                .ForMember(dst => dst.IsRefunded, opt =>
                    opt.MapFrom(src => src.IsRefunded))
                .ForMember(dst => dst.IsDevVersionBot, opt =>
                    opt.MapFrom(src => src.IsDevVersionBot))
                .ForMember(dst => dst.TelegramUserId, opt =>
                    opt.MapFrom(src => src.TelegramUserId))
                .ForMember(dst => dst.GoodReplacementType, opt =>
                    opt.MapFrom(src => src.GoodReplacementType));
        }
    }
}
