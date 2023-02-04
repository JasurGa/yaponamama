using FluentValidation;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionPagedList
{
    public class GetCorrectionPagedListQueryValidator : AbstractValidator<GetCorrectionPagedListQuery>
    {
        public GetCorrectionPagedListQueryValidator()
        {
            RuleFor(x => x.PageSize)
                .NotEmpty();
        }
    }
}
