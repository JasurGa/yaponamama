using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Linq;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderPagedList
{
    public class OrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public OrderCourierLookupDto Courier { get; set; }

        public float Price { get; set; }

        public int Status { get; set; }

        public int GoodCount { get; set; }

        public bool IsPrePayed { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Courier, opt =>
                    opt.MapFrom(src => src.Courier))
                .ForMember(dst => dst.Price, opt =>
                    opt.MapFrom(src => src.SellingPrice))
                .ForMember(dst => dst.GoodCount, opt =>
                    opt.MapFrom(src => src.GoodToOrders.Sum(x => x.Count)))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt));
        }
    }
}
