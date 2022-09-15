using System;
using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatMessagesForUser
{
    public class ChatMessageLookupDto : IMapWith<ChatMessage>
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public int MessageType { get; set; }

        public string Body { get; set; }

        public string Optional { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool HasBeenRead { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChatMessage, ChatMessageLookupDto>()
                .ForMember(dst => dst.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FromUserId, opt =>
                    opt.MapFrom(src => src.FromUserId))
                .ForMember(dst => dst.ToUserId, opt =>
                    opt.MapFrom(src => src.ToUserId))
                .ForMember(dst => dst.MessageType, opt =>
                    opt.MapFrom(src => src.MessageType))
                .ForMember(dst => dst.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dst => dst.Optional, opt =>
                    opt.MapFrom(src => src.Optional))
                .ForMember(dst => dst.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt))
                .ForMember(dst => dst.HasBeenRead, opt =>
                    opt.MapFrom(src => src.HasBeenRead));
        }
    }
}
