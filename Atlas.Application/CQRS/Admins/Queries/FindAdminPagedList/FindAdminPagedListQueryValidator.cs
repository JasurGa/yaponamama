using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Admins.Queries.FindAdminPagedList
{
    public class FindAdminPagedListQueryValidator : AbstractValidator<FindAdminPagedListQuery>
    {
        public FindAdminPagedListQueryValidator()
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

