using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.GetStorePagedList
{
    public class GetStorePagedListQueryValidator : AbstractValidator<GetStorePagedListQuery>
    {
        public GetStorePagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
