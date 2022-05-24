using FluentValidation;

namespace Atlas.Application.CQRS.Users.Queries.GetUserPagedList
{
    public class GetUserPagedListQueryValidator : AbstractValidator<GetUserPagedListQuery>
    {
        public GetUserPagedListQueryValidator()
        {
            RuleFor(u => u.ShowDeleted)
                .NotEmpty();

            RuleFor(u => u.PageSize)
                .NotEmpty();
        }
    }
}
