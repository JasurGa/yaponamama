using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails
{
    public class OrderFeedbackDetailsVm : IMapWith<OrderFeedback>
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Rating { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderFeedback, OrderFeedbackDetailsVm>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.OrderId, opt =>
                    opt.MapFrom(x => x.OrderId))
                .ForMember(x => x.Rating, opt =>
                    opt.MapFrom(x => x.Rating))
                .ForMember(x => x.Text, opt =>
                    opt.MapFrom(x => x.Text))
                .ForMember(x => x.CreatedAt, opt =>
                    opt.MapFrom(x => x.CreatedAt));
        }
    }
}
