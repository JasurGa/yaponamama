using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage;
using Atlas.Domain;
using Atlas.WebApi.Hubs.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Atlas.WebApi.Hubs
{
    [Authorize]
    [SignalRHub]
    public class ChatHub : Hub
    {
        private static List<ConnectedUser> ConnectedUsers = new List<ConnectedUser>();

        private readonly IMediator _mediator;

        internal Guid UserId => !Context.User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(Context.User.FindFirst(TokenClaims.UserId).Value);

        internal string ConnectionId => Context.ConnectionId;

        public ChatHub(IMediator mediator) =>
            _mediator = mediator;

        public override Task OnConnectedAsync()
        {
            var userId = UserId;
            if (userId.Equals(Guid.Empty))
            {
                Clients.Caller.SendAsync("onCantConnect", new
                {
                    ConnectionId = ConnectionId,
                });

                return base.OnConnectedAsync();
            }

            ConnectedUsers.RemoveAll(x => x.UserId == userId);
            if (!ConnectedUsers.Any(x => x.ConnectionId == ConnectionId))
            {
                ConnectedUsers.Add(new ConnectedUser
                {
                    ConnectionId = ConnectionId,
                    UserId       = userId
                });

                Clients.Caller.SendAsync("onConnected", new
                {
                    ConnectionId   = ConnectionId,
                    UserId         = userId,
                    ConnectedUsers = ConnectedUsers
                });

                Clients.AllExcept(ConnectionId).SendAsync("onNewUserConnected", new
                {
                    ConnectectionId = ConnectionId,
                    UserId          = userId
                });
            }

            return base.OnConnectedAsync();
        }

        public async Task<ChatMessage> Send(string body, string optional, int messageType, Guid userId)
        {
            var myUserId = ConnectedUsers.FirstOrDefault(x =>
                x.ConnectionId == ConnectionId);

            if (myUserId == null || myUserId.UserId == userId)
            {
                await Clients.Caller.SendAsync("onCantSend", new
                {
                    ConnectionId = ConnectionId,
                });

                return null;
            }

            var vm = await _mediator.Send(new CreateChatMessageCommand
            {
                Body        = body,
                Optional    = optional,
                MessageType = messageType,
                ToUserId    = userId,
                FromUserId  = myUserId.UserId,
            });

            var message = new ChatMessage
            {
                Id          = vm,
                Body        = body,
                Optional    = optional,
                MessageType = messageType,
                ToUserId    = userId,
                FromUserId  = myUserId.UserId,
                CreatedAt   = DateTime.UtcNow,
            };

            var toUserIds = ConnectedUsers.Where(x =>
                x.UserId == userId);

            foreach (var toUserId in toUserIds)
            {
                await Clients.Client(toUserId.ConnectionId).SendAsync("onNewMessage", message);
            }

            return message;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var item = ConnectedUsers.FirstOrDefault(x =>
                x.ConnectionId == ConnectionId);

            if (item != null)
            {
                Clients.Others.SendAsync("onUserDisconnected", new
                {
                    ConnectionId = ConnectionId,
                    UserId       = item.UserId
                });
            }

            ConnectedUsers.RemoveAll(x => x.ConnectionId == ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
