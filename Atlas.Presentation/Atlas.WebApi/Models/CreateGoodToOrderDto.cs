using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.GoodToOrders.Commands.CreateGoodToOrder;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateGoodToOrderDto : IMapWith<CreateGoodToOrderCommand>
    {
        public Guid GoodId { get; set; }

        public int Count { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGoodToOrderDto, CreateGoodToOrderDto>()
                .ForMember(dst => dst.GoodId, opt =>
                    opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.Count, opt =>
                    opt.MapFrom(src => src.Count));
        }
    }
}
