﻿using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Orders.Queries.GetBotOrdersPagedList
{
    public class BotOrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public float TotalPrice { get; set; }

        public bool IsPickup { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeliverAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public List<BotGoodToOrderLookupDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, BotOrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.TotalPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice + src.ShippingPrice))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}