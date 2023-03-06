using System;
using System.Collections.Generic;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByClient
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

        public Guid? CourierId { get; set; }

        public Guid ClientId { get; set; }

        public Guid StoreId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public string Address { get; set; }

        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public long PurchasePrice { get; set; }

        public long SellingPrice { get; set; }

        public int PaymentType { get; set; }

        public bool IsPrePayed { get; set; }

        public int Status { get; set; }

        public int GoodReplacementType { get; set; }

        public IList<GoodToOrderLookupDto> GoodToOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.ExternalId, opt =>
                    opt.MapFrom(src => src.ExternalId))
                .ForMember(dst => dst.CourierId, opt =>
                    opt.MapFrom(src => src.CourierId))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.MapFrom(src => src.ClientId))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.FinishedAt, opt =>
                    opt.MapFrom(src => src.FinishedAt))
                .ForMember(dst => dst.Address, opt =>
                    opt.MapFrom(src => src.Address))
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.PurchasePrice, opt =>
                    opt.MapFrom(src => (long)Math.Ceiling(src.PurchasePrice)))
                .ForMember(dst => dst.SellingPrice, opt =>
                    opt.MapFrom(src => (long)Math.Ceiling(src.SellingPrice)))
                .ForMember(dst => dst.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType))
                .ForMember(dst => dst.IsPrePayed, opt =>
                    opt.MapFrom(src => src.IsPrePayed))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.GoodToOrders, opt =>
                    opt.MapFrom(src => src.GoodToOrders))
                .ForMember(dst => dst.GoodReplacementType, opt =>
                    opt.MapFrom(src => src.GoodReplacementType));
        }
    }
}
