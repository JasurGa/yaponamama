using System;
using MediatR;

namespace Atlas.Application.CQRS.OrderFeedbacks.Queries.GetOrderFeedbackDetails
{
    public class GetOrderFeedbackDetailsQuery : IRequest<OrderFeedbackDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
