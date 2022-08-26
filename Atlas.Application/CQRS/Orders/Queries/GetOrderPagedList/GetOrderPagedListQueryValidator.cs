using FluentValidation;

namespace Atlas.Application.CQRS.Orders.Queries.GetLastOrdersPagedListByAdmin
{
    public class GetOrderPagedListQueryValidator : AbstractValidator<GetOrderPagedListQuery>
    {
        public GetOrderPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
