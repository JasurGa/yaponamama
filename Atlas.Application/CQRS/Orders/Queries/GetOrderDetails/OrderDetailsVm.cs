using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Queries.GetClientDetails;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierDetails;
using Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList;
using Atlas.Application.CQRS.Promos.Queries.GetPromoDetails;
using Atlas.Application.CQRS.Stores.Queries.GetStoreDetails;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class OrderDetailsVm : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int DestinationType { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public float SellingPrice { get; set; }

        public float PurchasePrice { get; set; }

        public CourierDetailsVm Courier { get; set; }

        public ClientDetailsVm Client { get; set; }

        public PromoDetailsVm Promo { get; set; }

        public PaymentTypeLookupDto PaymentType { get; set; }

        public StoreDetailsVm Store { get; set; }

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
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => src.PurchasePrice));
        }
    }
}
