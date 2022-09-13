using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Supports.Queries.FindSupportsPagedList
{
    public class FindSupportsPagedListQueryValidator : AbstractValidator<FindSupportsPagedListQuery>
    {
        public FindSupportsPagedListQueryValidator()
        {
            RuleFor(x => x.SearchQuery)
                .NotNull();

            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
        }
    }
}

