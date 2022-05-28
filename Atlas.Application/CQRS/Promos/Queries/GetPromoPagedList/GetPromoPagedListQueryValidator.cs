using FluentValidation;

namespace Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList
{
    public class GetPromoPagedListQueryValidator : AbstractValidator<GetPromoPagedListQuery>
    {
        public GetPromoPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
