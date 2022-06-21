using System;
using FluentValidation;

namespace Atlas.Application.CQRS.PageVisits.Queries.GetPagesVisits
{
    public class GetPagesVisitsQueryValidator :
        AbstractValidator<GetPagesVisitsQuery>
    {
        public GetPagesVisitsQueryValidator()
        {
            RuleFor(x => x.Pages)
                .NotNull();
        }
    }
}
