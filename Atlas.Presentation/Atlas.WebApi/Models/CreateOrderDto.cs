using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.CreateOrder;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public string Apartment { get; set; }

        public string Floor { get; set; }

        public string Entrance { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public int PaymentType { get; set; }

        public string Promo { get; set; }

        public DateTime? DeliverAt { get; set; }

        public IEnumerable<CreateGoodToOrderDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
                .ForMember(dst => dst.Comment, opt =>
                    opt.MapFrom(src => src.Comment))
                .ForMember(dst => dst.DontCallWhenDelivered, opt =>
                    opt.MapFrom(src => src.DontCallWhenDelivered))
                .ForMember(dst => dst.Apartment, opt =>
                    opt.MapFrom(src => src.Apartment))
                .ForMember(dst => dst.Floor, opt =>
                    opt.MapFrom(src => src.Floor))
                .ForMember(dst => dst.Entrance, opt =>
                    opt.MapFrom(src => src.Entrance))
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.Promo, opt =>
                    opt.MapFrom(src => src.Promo))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}
