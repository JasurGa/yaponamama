using System;
using Atlas.Application.Models;
using MediatR;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatMessagesForUser
{
    public class GetChatMessagesForUserQuery : IRequest<PageDto<ChatMessageLookupDto>>
    {
        public Guid MyUserId { get; set; }

        public Guid ChatUserId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
