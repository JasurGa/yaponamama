using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderPagedList
{
    public class GetProviderPagedListQueryValidator : AbstractValidator<GetProviderPagedListQuery>
    {
        public GetProviderPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
