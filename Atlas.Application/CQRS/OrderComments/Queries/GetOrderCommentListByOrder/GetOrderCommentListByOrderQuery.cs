using MediatR;
using System;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class GetOrderCommentListByOrderQuery : IRequest<OrderCommentListVm>
    {
        public Guid OrderId { get; set; }
    }
}
