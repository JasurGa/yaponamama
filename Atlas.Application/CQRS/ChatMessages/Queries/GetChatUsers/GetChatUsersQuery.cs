using System;
using MediatR;

namespace Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers
{
    public class GetChatUsersQuery : IRequest<ChatUsersListVm>
    {
        public Guid UserId { get; set; }
    }
}
