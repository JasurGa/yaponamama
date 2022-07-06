using FluentValidation;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodPagedList
{
    public class GetGoodPagedListQueryValidator :
        AbstractValidator<GetGoodPagedListQuery>
    {
        public GetGoodPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        } 
    }
}
