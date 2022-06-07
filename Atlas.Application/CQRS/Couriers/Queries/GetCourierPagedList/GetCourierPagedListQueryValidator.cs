using FluentValidation;

namespace Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList
{
    public class GetCourierPagedListQueryValidator : AbstractValidator<GetCourierPagedListQuery>
    {
        public GetCourierPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
