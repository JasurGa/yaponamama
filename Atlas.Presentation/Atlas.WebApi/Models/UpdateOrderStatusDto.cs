using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderStatus;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class UpdateOrderStatusDto : IMapWith<UpdateOrderStatusCommand>
    {
        public Guid Id { get; set; }

        public int Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderStatusDto, UpdateOrderStatusCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Status, opt =>
                    opt.MapFrom(src => src.Status));
        }
    }
}

