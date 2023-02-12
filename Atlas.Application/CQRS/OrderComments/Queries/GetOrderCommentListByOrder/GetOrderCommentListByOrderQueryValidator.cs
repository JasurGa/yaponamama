using FluentValidation;
using System;

namespace Atlas.Application.CQRS.OrderComments.Queries.GetOrderCommentListByOrder
{
    public class GetOrderCommentListByOrderQueryValidator : AbstractValidator<GetOrderCommentListByOrderQuery>
    {
        public GetOrderCommentListByOrderQueryValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);
        }
    }
}
