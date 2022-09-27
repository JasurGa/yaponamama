﻿using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Queries;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
{
    public class ClientOrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public Guid? CourierId { get; set; }

        public Guid ClientId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public float SellingPrice { get; set; }

        public int Status { get; set; }

        public string StoreName { get; set; }

        public string StoreNameRu { get; set; }

        public string StoreNameEn { get; set; }

        public string StoreNameUz { get; set; }

        public int Apartment { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public int PaymentType { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public string Comment { get; set; }

        public bool IsPrePayed { get; set; }

        public IList<GoodToOrderLookupDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, ClientOrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CourierId, opt =>
                    opt.MapFrom(src => src.CourierId))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
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
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.StoreName, opt =>
                    opt.MapFrom(src => src.Store.Name))
                .ForMember(dst => dst.StoreNameRu, opt =>
                    opt.MapFrom(src => src.Store.NameRu))
                .ForMember(dst => dst.StoreNameEn, opt =>
                    opt.MapFrom(src => src.Store.NameEn))
                .ForMember(dst => dst.StoreNameUz, opt =>
                    opt.MapFrom(src => src.Store.NameUz))
                .ForMember(dst => dst.Apartment, opt =>
                    opt.MapFrom(src => src.Apartment))
                .ForMember(dst => dst.Floor, opt =>
                    opt.MapFrom(src => src.Floor))
                .ForMember(dst => dst.Entrance, opt =>
                    opt.MapFrom(src => src.Entrance))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.DontCallWhenDelivered, opt =>
                    opt.MapFrom(src => src.DontCallWhenDelivered))
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}