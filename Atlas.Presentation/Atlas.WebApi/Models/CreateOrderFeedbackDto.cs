using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateOrderFeedbackDto : IMapWith<CreateOrderFeedbackCommand>
    {
        public Guid OrderId { get; set; }

        public string Rating { get; set; }

        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderFeedbackDto, CreateOrderFeedbackCommand>()
                .ForMember(p => p.OrderId, opt =>
                    opt.MapFrom(p => p.OrderId))
                .ForMember(p => p.Rating, opt =>
                    opt.MapFrom(p => p.Rating))
                .ForMember(p => p.Text, opt =>
                    opt.MapFrom(p => p.Text));
        }
    }
}
