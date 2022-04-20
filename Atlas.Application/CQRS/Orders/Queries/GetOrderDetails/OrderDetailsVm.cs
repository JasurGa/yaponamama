using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderDetails
{
    public class OrderDetailsVm : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public Guid? CourierId { get; set; }

        public Guid ClientId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsVm>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CourierId, opt =>
                    opt.MapFrom(src => src.CourierId))
                .ForMember(dest => dest.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dest => dest.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dest => dest.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dest => dest.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup));
        }
    }
}
