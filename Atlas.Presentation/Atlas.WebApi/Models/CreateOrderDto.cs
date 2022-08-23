using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.CreateOrder;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        public string? Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int DestinationType { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public Guid PaymentTypeId { get; set; }

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
                .ForMember(dst => dst.DestinationType, opt =>
                    opt.MapFrom(src => src.DestinationType))
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
                .ForMember(dst => dst.PaymentTypeId, opt =>
                    opt.MapFrom(src => src.PaymentTypeId))
                .ForMember(dst => dst.Promo, opt =>
                    opt.MapFrom(src => src.Promo))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders));
        }
    }
}
