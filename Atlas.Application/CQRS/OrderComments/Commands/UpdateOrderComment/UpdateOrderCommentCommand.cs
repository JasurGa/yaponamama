using MediatR;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.UpdateOrderComment
{
    public class UpdateOrderCommentCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Text { get; set; }
    }
}
