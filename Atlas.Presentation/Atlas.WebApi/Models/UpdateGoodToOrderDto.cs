using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Commands.UpdateGoodToOrder;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateGoodToOrderDto : IMapWith<UpdateGoodToOrderCommand>
    {
        public Guid Id { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGoodToOrderDto, UpdateGoodToOrderCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
