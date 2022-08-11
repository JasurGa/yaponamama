using System;
using Atlas.Application.Common.Mappings;
using Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage;
using AutoMapper;

namespace Atlas.WebApi.Models
{
    public class CreateChatMessageDto : IMapWith<CreateChatMessageCommand>
    {
        public Guid ToUserId { get; set; }

        public int MessageType { get; set; }

        public string Body { get; set; }

        public string Optional { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateChatMessageDto, CreateChatMessageCommand>()
                .ForMember(dst => dst.ToUserId, opt =>
                    opt.MapFrom(src => src.ToUserId))
                .ForMember(dst => dst.MessageType, opt =>
                    opt.MapFrom(src => src.MessageType))
                .ForMember(dst => dst.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.Optional, opt =>
                    opt.MapFrom(src => src.Optional));
        }
    }
}
