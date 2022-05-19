using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.GetStorePagedList
{
    public class GetStorePagedListQueryValidator : AbstractValidator<GetStorePagedListQuery>
    {
        public GetStorePagedListQueryValidator()
        {
            RuleFor(u => u.ShowDeleted)
                .NotEmpty();

            RuleFor(u => u.PageSize)
                .NotEmpty();
        }
    }
}
