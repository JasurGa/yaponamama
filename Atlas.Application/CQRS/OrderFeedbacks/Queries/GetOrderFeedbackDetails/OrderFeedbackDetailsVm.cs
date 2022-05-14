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
                .ForMember(of => of.Id,
                    opt => opt.MapFrom(e => e.Id))
                .ForMember(of => of.OrderId,
                    opt => opt.MapFrom(e => e.OrderId))
                .ForMember(of => of.Rating,
                    opt => opt.MapFrom(e => e.Rating))
                .ForMember(of => of.Text,
                    opt => opt.MapFrom(e => e.Text))
                .ForMember(of => of.CreatedAt,
                    opt => opt.MapFrom(e => e.CreatedAt));
        }
    }
}
