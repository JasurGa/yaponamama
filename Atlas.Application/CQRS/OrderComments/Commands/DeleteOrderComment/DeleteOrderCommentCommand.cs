using MediatR;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.DeleteOrderComment
{
    public class DeleteOrderCommentCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
