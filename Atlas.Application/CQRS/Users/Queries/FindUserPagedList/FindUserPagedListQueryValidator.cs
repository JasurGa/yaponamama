using System;
using FluentValidation;

namespace Atlas.Application.CQRS.Users.Queries.FindUserPagedList
{
    public class FindUserPagedListQueryValidator : AbstractValidator<FindUserPagedListQuery>
    {
        public FindUserPagedListQueryValidator()
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

