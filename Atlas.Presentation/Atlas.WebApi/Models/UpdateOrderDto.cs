using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrder;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        public Guid Id { get; set; }

        public Guid? CourierId { get; set; }

        public Guid StoreId { get; set; }

        public Guid ClientId { get; set; }

        public string Comment { get; set; }

        public bool DontCallWhenDelivered { get; set; }

        public int Apartment { get; set; }

        public int Floor { get; set; }

        public int Entrance { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeliverAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public float PurchasePrice { get; set; }

        public float SellingPrice { get; set; }

        public int Status { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public int PaymentType { get; set; }

        public bool IsPickup { get; set; }

        public Guid? PromoId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CourierId, opt =>
                    opt.MapFrom(src => src.CourierId))
                .ForMember(dst => dst.StoreId, opt =>
                    opt.MapFrom(src => src.StoreId))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
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
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.DeliverAt, opt =>
                    opt.MapFrom(src => src.DeliverAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => src.PurchasePrice))
                .ForMember(dst => dst.SellingPrice, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.PromoId, opt =>
                    opt.MapFrom(src => src.PromoId));
        }
    }
}
