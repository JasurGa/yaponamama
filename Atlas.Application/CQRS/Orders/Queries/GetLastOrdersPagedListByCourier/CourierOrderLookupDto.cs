﻿using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByCourier
{
    public class CourierOrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public int OrderCode
        {
            get
            {
                return (int)(Convert.ToInt64(Id.ToString().Split("-")[0], 16) % 1000000);
            }
        }

        public Guid? CourierId { get; set; }

        public Guid ClientId { get; set; }

        public string Address { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public float PurchasePrice { get; set; }

        public float SellingPrice { get; set; }

        public float ShippingPrice { get; set; }

        public float TotalPrice
        {
            get
            {
                return SellingPrice + ShippingPrice;
            }
        }

        public string Comment { get; set; }

        public string Apartment { get; set; }

        public string Floor { get; set; }

        public string Entrance { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int Status { get; set; }

        public bool IsPrePayed { get; set; }

        public int PaymentType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeliverAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public CourierOrderStoreDetailsVm Store { get; set; }

        public CourierOrderClientDetailsVm Client { get; set; }

        public IList<GoodToOrderLookupDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, CourierOrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CourierId, opt =>
                    opt.MapFrom(src => src.CourierId))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.Address, opt =>
                    opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dst => dst.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dst => dst.ShippingPrice, opt =>
                    opt.MapFrom(src => src.ShippingPrice))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.Apartment, opt =>
                    opt.MapFrom(src => src.Apartment))
                .ForMember(dst => dst.Floor, opt =>
                    opt.MapFrom(src => src.Floor))
                .ForMember(dst => dst.Entrance, opt =>
                    opt.MapFrom(src => src.Entrance))
                .ForMember(dst => dst.DontCallWhenDelivered, opt =>
                    opt.MapFrom(src => src.DontCallWhenDelivered))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.Client, opt =>
                    opt.MapFrom(src => src.Client))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}