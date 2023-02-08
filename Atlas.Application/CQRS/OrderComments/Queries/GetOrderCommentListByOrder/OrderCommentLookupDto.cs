using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class OrderCommentLookupDto : IMapWith<OrderComment>
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public OrderCommentUserLookupDto User { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderComment, OrderCommentLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderId, opt =>
                    opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.UserId, opt =>
                    opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.User, opt =>
                    opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Text, opt =>
                    opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt));
        }
    }
}
