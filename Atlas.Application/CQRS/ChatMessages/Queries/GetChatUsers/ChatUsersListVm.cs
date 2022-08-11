using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{
    public class ChatUsersListVm
    {
        public List<ChatUserLookupDto> ChatUsers { get; set; }
    }
}
