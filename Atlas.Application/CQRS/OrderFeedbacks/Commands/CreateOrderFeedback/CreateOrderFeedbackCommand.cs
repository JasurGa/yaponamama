using System;
using MediatR;

namespace Atlas.Application.CQRS.OrderFeedbacks.Commands.CreateOrderFeedback
{
    public class CreateOrderFeedbackCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }

        public string Rating { get; set; }

        public string Text { get; set; }
    }
}
