using FluentValidation;

namespace Atlas.Application.CQRS.Users.Queries.GetUserPagedList
{
    public class GetUserPagedListQueryValidator : AbstractValidator<GetUserPagedListQuery>
    {
        public GetUserPagedListQueryValidator()
        {
            RuleFor(x => x.ShowDeleted)
                .NotEmpty();

            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
