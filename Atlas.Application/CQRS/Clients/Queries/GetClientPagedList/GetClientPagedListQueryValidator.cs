using FluentValidation;

namespace Atlas.Application.CQRS.Clients.Queries.GetClientPagedList
{
    public class GetClientPagedListQueryValidator : AbstractValidator<GetClientPagedListQuery>
    {
        public GetClientPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
