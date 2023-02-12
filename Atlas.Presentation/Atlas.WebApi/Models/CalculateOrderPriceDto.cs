using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using Atlas.Application.CQRS.Orders.Queries.CalculateOrderPrice;
using AutoMapper;
using System.Collections.Generic;

namespace Atlas.WebApi.Models
{
    public class CalculateOrderPriceDto : IMapWith<CalculateOrderPriceQuery>
    {
        public float ToLongitude { get; set; }

        public float ToLatitude { get; set; }

        public bool IsPickup { get; set; }

        public string Promo { get; set; }

        public IEnumerable<CreateGoodToOrderCommand> GoodToOrders { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CalculateOrderPriceDto, CalculateOrderPriceQuery>()
                .ForMember(dst => dst.ToLongitude, opt =>
                    opt.MapFrom(src => src.ToLongitude))
                .ForMember(dst => dst.ToLatitude, opt =>
                    opt.MapFrom(src => src.ToLatitude))
                .ForMember(dst => dst.IsPickup, opt =>
                    opt.MapFrom(src => src.IsPickup))
                .ForMember(dst => dst.Promo, opt =>
                    opt.MapFrom(src => src.Promo))
                .ForMember(dst => dst.ClientId, opt =>
                    opt.Ignore());
        }
    }
}
