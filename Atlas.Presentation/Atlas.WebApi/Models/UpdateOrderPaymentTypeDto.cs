using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.Orders.Commands.UpdateOrderPaymentType;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateOrderPaymentTypeDto : IMapWith<UpdateOrderPaymentTypeCommand>
    {
        public Guid Id { get; set; }

        public int PaymentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderPaymentTypeDto, UpdateOrderPaymentTypeCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.PaymentType, opt =>
                    opt.MapFrom(src => src.PaymentType));
        }
    }
}
