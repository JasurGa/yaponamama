using FluentValidation;

namespace Atlas.Application.CQRS.GoodToOrders.Queries.GetGoodToOrderPagedList
{
    public class GetGoodToOrderPagedListQueryValidator : AbstractValidator<GetGoodToOrderPagedListQuery>
    {
        public GetGoodToOrderPagedListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .NotEmpty();
        }
    }
}
