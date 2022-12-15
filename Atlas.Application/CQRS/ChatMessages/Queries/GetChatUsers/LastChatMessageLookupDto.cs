using Atlas.Application.Common.Mappings;
using Atlas.Domain;
using AutoMapper;
using System;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{
    public class LastChatMessageLookupDto : IMapWith<ChatMessage>
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChatMessage, LastChatMessageLookupDto>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Body, opt =>
                    opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(src => src.CreatedAt));
        }
    }
}
