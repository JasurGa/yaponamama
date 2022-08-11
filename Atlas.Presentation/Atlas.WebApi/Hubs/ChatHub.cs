using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage;
using Atlas.Domain;
using Atlas.WebApi.Hubs.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Atlas.WebApi.Hubs
{
    [SignalRHub]
    public class ChatHub : Hub
    {
        private static List<ConnectedUser> ConnectedUsers = new List<ConnectedUser>();

        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator) =>
            _mediator = mediator;

        public void Connect()
        {
            var userIdClaim = Context.User.Claims.Where(x => x.Type == TokenClaims.UserId)
                .Select(x => x.Value).FirstOrDefault();

            if (userIdClaim == null)
            {
                return;
            }

            var id     = Context.ConnectionId;
            var userId = Guid.Parse(userIdClaim);

            if (!ConnectedUsers.Any(x => x.ConnectionId == id))
            {
                ConnectedUsers.Add(new ConnectedUser
                {
                    ConnectionId = id,
                    UserId       = userId
                });

                Clients.Caller.SendAsync("onConnected", new
                {
                    ConnectionId   = id,
                    UserId         = userId,
                    ConnectedUsers = ConnectedUsers
                });

                Clients.AllExcept(id).SendAsync("onNewUserConnected", new
                {
                    ConnectectionId = id,
                    UserId          = userId
                });
            }
        }

        public async Task Send(string body, string optional, int messageType, Guid userId)
        {
            var myUserId = ConnectedUsers.FirstOrDefault(x =>
                x.ConnectionId == Context.ConnectionId);

            if (myUserId == null)
            {
                return;
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
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id   = Context.ConnectionId;
            var item = ConnectedUsers.FirstOrDefault(x =>
                x.ConnectionId == id);

            if (item != null)
            {
                ConnectedUsers.Remove(item);
                Clients.All.SendAsync("onUserDisconnected", new
                {
                    ConnectionId = id,
                    UserId       = item.UserId
                });
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
