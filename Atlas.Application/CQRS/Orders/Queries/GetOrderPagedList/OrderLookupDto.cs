using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Domain;
using AutoMapper;
using System;
using System.Linq;

namespace Atlas.Application.CQRS.Orders.Queries.GetOrderPagedList
{
    public class OrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }

        public string ExternalId { get; set; }

        public int OrderCode
        {
            get
            {
                return (int)(Convert.ToInt64(Id.ToString().Split("-")[0], 16) % 1000000);
            }
        }

        public ClientLookupDto Client { get; set; }

        public OrderCourierLookupDto Courier { get; set; }

        public long Price { get; set; }

        public long SellingPrice { get; set; }

        public long ShippingPrice { get; set; }

        public long PurchasePrice { get; set; }

        public long TotalPrice
        {
            get
            {
                return ShippingPrice + SellingPrice;
            }
        }

        public int Status { get; set; }

        public int GoodCount { get; set; }

        public int PaymentType { get; set; }

        public bool IsPrePayed { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ExternalId, opt =>
                    opt.MapFrom(src => src.ExternalId))
                .ForMember(dst => dst.Client, opt =>
                    opt.MapFrom(src => src.Client))
                .ForMember(dst => dst.Courier, opt =>
                    opt.MapFrom(src => src.Courier))
                .ForMember(dst => dst.Price, opt =>
                    opt.MapFrom(src => (long)(Math.Ceiling(src.SellingPrice) + Math.Ceiling(src.ShippingPrice))))
                .ForMember(dst => dst.SellingPrice, opt =>
                    opt.MapFrom(src => (long)Math.Ceiling(src.SellingPrice)))
                .ForMember(dst => dst.ShippingPrice, opt =>
                    opt.MapFrom(src => (long)Math.Ceiling(src.ShippingPrice)))
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => (long)Math.Ceiling(src.PurchasePrice)))
                .ForMember(dst => dst.GoodCount, opt =>
                    opt.MapFrom(src => src.GoodToOrders.Sum(x => x.Count)))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt));
        }
    }
}
