using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.OrderComments.Commands.CreateOrderComment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class CreateOrderCommentDto : IMapWith<CreateOrderCommentCommand>
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommentDto, CreateOrderCommentCommand>()
                .ForMember(dst => dst.OrderId, opt =>
                    opt.MapFrom(src => src.OrderId))
                .ForMember(dst => dst.UserId, opt =>
                    opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.Text, opt =>
                    opt.MapFrom(src => src.Text));
        }
    }
}
