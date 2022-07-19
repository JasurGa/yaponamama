using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList
{
    public class GetAdminPagedListQueryHandlerValidator : AbstractValidator<GetAdminPagedListQuery>
    {
        public GetAdminPagedListQueryHandlerValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
