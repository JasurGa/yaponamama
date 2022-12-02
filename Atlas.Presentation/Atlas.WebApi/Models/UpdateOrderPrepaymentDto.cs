using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderPrepayment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateOrderPrepaymentDto : IMapWith<UpdateOrderPrepaymentCommand>
    {
        public Guid Id { get; set; }

        public bool IsPrepaid { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderPrepaymentDto, UpdateOrderPrepaymentCommand>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.IsPrepaid, opt =>
                    opt.MapFrom(src => src.IsPrepaid));
        }
    }
}
