using MediatR;
using System;

namespace Atlas.Application.CQRS.OrderComments.Commands.CreateOrderComment
{
    public class CreateOrderCommentCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public string Text { get; set; }
    }
}
