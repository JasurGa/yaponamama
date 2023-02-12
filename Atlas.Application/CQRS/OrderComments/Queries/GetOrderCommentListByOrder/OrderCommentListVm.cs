using System.Collections.Generic;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class OrderCommentListVm
    {
        public IList<OrderCommentLookupDto> OrderComments { get; set; }
    }
}
