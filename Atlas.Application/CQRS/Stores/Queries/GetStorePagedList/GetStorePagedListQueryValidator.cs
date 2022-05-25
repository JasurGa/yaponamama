using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.GetStorePagedList
{
    public class GetStorePagedListQueryValidator : AbstractValidator<GetStorePagedListQuery>
    {
        public GetStorePagedListQueryValidator()
        {
            RuleFor(u => u.ShowDeleted)
                .Must(sd => sd == true || sd == false);

            RuleFor(u => u.PageSize)
                .NotEmpty();
        }
    }
}
