using FluentValidation;
using System;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderListByOrder
{
    public class GetGoodToOrderListByOrderQueryValidator : AbstractValidator<GetGoodToOrderListByOrderQuery>
    {
        public GetGoodToOrderListByOrderQueryValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty);
        }
    }
}
