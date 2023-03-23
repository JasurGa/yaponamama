using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.PayOrder;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class PayOrderDto : IMapWith<PayOrderCommand>
    {
        public string Token { get; set; }

        public Guid OrderId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PayOrderDto, PayOrderCommand>()
                .ForMember(dst => dst.Token, opt =>
                    opt.MapFrom(src => src.Token))
                .ForMember(dst => dst.OrderId, opt =>
                    opt.MapFrom(src => src.OrderId));
        }
    }
}
