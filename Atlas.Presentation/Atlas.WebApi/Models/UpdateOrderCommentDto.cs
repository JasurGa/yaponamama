using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.OrderComments.Commands.UpdateOrderComment;
using AutoMapper;
using System;

namespace Atlas.WebApi.Models
{
    public class UpdateOrderCommentDto : IMapWith<UpdateOrderCommentCommand>
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderCommentDto, UpdateOrderCommentCommand>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id))
                .ForMember(x => x.Text, opt =>
                    opt.MapFrom(x => x.Text));
        }
    }
}
