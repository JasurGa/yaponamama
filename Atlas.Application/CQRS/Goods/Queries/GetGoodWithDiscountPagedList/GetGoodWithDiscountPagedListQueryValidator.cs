using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodWithDiscountPagedList
{
    public class GetGoodWithDiscountPagedListQueryValidator : AbstractValidator<GetGoodWithDiscountPagedListQuery>
    {
        public GetGoodWithDiscountPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
